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
    Max // 마지막 인덱스 확인용. 실제로 클립을 할당하지 않음.
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Tooltip("사용할 AudioClip을 ESoundType 열거형에 맞춰서 넣어주세요.")]
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
            Debug.LogWarning("AudioManager에 AudioSource 컴포넌트가 없습니다. AudioSource 컴포넌트를 하나 생성합니다.");

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

        Debug.Log("현재 모든 AudioSource가 사용중입니다. AudioManager에 Source 컴포넌트를 새로 생성 및 추가합니다.");

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSources.Add(audioSource);

        return audioSource;
    }

    public void PlaySound(ESoundType eSoundType) 
    {
        if (eSoundType >= ESoundType.Max) 
        {
            Debug.LogWarning("재생하려는 SoundType이 Max값 이상입니다. 확인해주세요.");
            return;
        }
        if (audioClips.Length <= (int)eSoundType) 
        {
            Debug.LogWarning("재생하려는 AudioClip이 없습니다. AudioManager에 추가해주세요.");
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
