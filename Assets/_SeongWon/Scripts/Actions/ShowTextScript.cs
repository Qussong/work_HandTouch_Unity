using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class ShowTextScript : MonoBehaviour, ActionScriptInterface
{
    [Header("Custom Peoperty")]
    [Tooltip("When this time is reached the image will be displayed.")]
    [SerializeField] float startTime;
    [Tooltip("Once this time is reached the image will disappear.")]
    [SerializeField] float EndTime;

    TextMeshProUGUI targetImage;
    float timer = 0;
    bool IsPlay = false;
    public bool StartTimer = false;

    void Awake()
    {
        targetImage = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (StartTimer)
        {
            timer += Time.deltaTime;

            if (timer >= startTime && timer < EndTime)
            {
                targetImage.enabled = true;
            }
            else if (timer >= EndTime)
            {
                targetImage.enabled = false;
                StartTimer = false;
                timer = 0;
                IsPlay = false;
            }
        }

    }

    public void StartAction()
    {
        if (!IsPlay)
        {
            StartTimer = true;
        }
    }

    public void StopAction()
    {
        timer = EndTime;
    }

}
