using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayMainMetapo : MonoBehaviour
{
    public static PlayMainMetapo instance;

    [SerializeField] float delayTime;
    [SerializeField] GameObject targetGameObject;
    [SerializeField] AudioClip audioClip;
    [SerializeField] GameObject handParentObject;
    [SerializeField] float alhpaChangeSpeed;

    AudioSource audioSource;
    private Image[] handimages;
    private bool isAlphazero;
    float delayTimer = 0;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.Play();
        targetGameObject.SetActive(true);

        isAlphazero = false;
        handimages = handParentObject.GetComponentsInChildren<Image>();

    }
    void Update()
    {
        if (audioSource.isPlaying)
        {
            StartBlinkImage();
            return;
        }
        else 
        {
            StopBlinkImage();
            targetGameObject.SetActive(false);
        }

            delayTimer += Time.deltaTime;

        if (delayTimer >= delayTime) 
        {
            delayTimer = 0.0f;
            audioSource.Play();
            targetGameObject.SetActive(true);
        }      
    }

    public void StopMainMetapo() 
    {
        if (audioSource.isPlaying) 
        {
            audioSource.Stop();
        }

        delayTimer = 0.0f;
        audioSource.time = 0.0f;
        targetGameObject.SetActive(false);
        StopBlinkImage();
    }

    void StartBlinkImage() 
    {
        Color color = handimages[0].color;

        if (!isAlphazero)
        {
            color.a -= Time.deltaTime * alhpaChangeSpeed;

            if (color.a <= 0.0f)
                isAlphazero = true;
        }
        else 
        {
            color.a += Time.deltaTime * alhpaChangeSpeed;

            if (color.a >= 1.0f)
                isAlphazero = false;
        }

        foreach (var item in handimages)
        {
            item.color = color;
        }
    }

    void StopBlinkImage() 
    {
        Color color = handimages[0].color;
        color.a = 0.0f;

        isAlphazero = true;

        foreach (var item in handimages)
        {
            item.color = color;
        }
    }
}
