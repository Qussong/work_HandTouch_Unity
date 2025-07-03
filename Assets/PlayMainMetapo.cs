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
    float imageDelayTimer = 0;
    float imageDelayTime = 11.0f;
    bool stopBlink;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.Play();
        targetGameObject.SetActive(true);

        stopBlink = false;
        isAlphazero = false;
        handimages = handParentObject.GetComponentsInChildren<Image>();
        StartBlinkImage();

    }
    void Update()
    {
        if (audioSource.isPlaying)
        {
            return;
        }
        else
        {
            targetGameObject.SetActive(false);
        }

        delayTimer += Time.deltaTime;
        imageDelayTimer += Time.deltaTime;

        if (imageDelayTimer > imageDelayTime)
        {
            imageDelayTimer = 0.0f;

            StartBlinkImage();
        }

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
    }

    public void StartBlinkImage()
    {
        Color color = handimages[0].color;
        color.a = 0.3f;

        isAlphazero = true;

        foreach (var item in handimages)
        {
            item.color = color;
        }

        imageDelayTimer = 0.0f;
    }

    public void StopBlinkImage()
    {
        Color color = handimages[0].color;
        color.a = 0.0f;

        isAlphazero = true;

        foreach (var item in handimages)
        {
            item.color = color;
        }

        imageDelayTimer = 0.0f;
    }
}