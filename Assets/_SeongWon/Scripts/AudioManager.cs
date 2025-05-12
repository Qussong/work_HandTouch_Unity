using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum ESoundType
{
    Walk,
    Click,
    Cannon,
    Clock,
    Max // ������ �ε��� Ȯ�ο�. ������ Ŭ���� �Ҵ����� ����.
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Tooltip("����� AudioClip�� ESoundType �������� ���缭 �־��ּ���.")]
    [SerializeField] private AudioClip[] audioClips;
    
    private List<AudioSource> audioSources;

    void Awake() 
    {
        if (instance == null)
        {
            instance = this;
            CustomAwake();
            DontDestroyOnLoad(gameObject);
        }
        else 
        {
            Destroy(gameObject);
            return;
        }
    }

    void CustomAwake() 
    {
        audioSources = new List<AudioSource>();

        AudioSource[] HasAudioSources = GetComponents<AudioSource>();

        if (HasAudioSources.Length <= 0) 
        {
            Debug.LogWarning("AudioManager�� AudioSource ������Ʈ�� �����ϴ�. AudioSource ������Ʈ�� �ϳ� �����մϴ�.");

            AudioSource audioSource = null;
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSources.Add(audioSource);
        }
        else
        {
            foreach (var item in HasAudioSources)
            {
                item.playOnAwake = false;
                audioSources.Add(item);
            }
        }
    }

    AudioSource FindNonPlayingSource() 
    {
        AudioSource audioSource = null;

        foreach (var source in audioSources)
        {
            if (!source.isPlaying) 
            {
                audioSource = source;
                return audioSource;
            }
        }

        Debug.Log("���� ��� AudioSource�� ������Դϴ�. AudioManager�� Source ������Ʈ�� ���� ���� �� �߰��մϴ�.");

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSources.Add(audioSource);

        return audioSource;
    }

    public void PlaySound(ESoundType eSoundType) 
    {
        if (eSoundType >= ESoundType.Max) 
        {
            Debug.LogWarning("����Ϸ��� SoundType�� Max�� �̻��Դϴ�. Ȯ�����ּ���.");
            return;
        }
        if (audioClips.Length <= (int)eSoundType) 
        {
            Debug.LogWarning("����Ϸ��� AudioClip�� �����ϴ�. AudioManager�� �߰����ּ���.");
            return;
        }

        AudioSource audio = FindNonPlayingSource();

        if (audio != null)
        {           
            audio.clip = audioClips[(int)eSoundType];
            audio.Play();
        }
    }

}
