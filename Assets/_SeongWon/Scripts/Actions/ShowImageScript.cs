using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class ShowImageScript : MonoBehaviour, ActionScriptInterface
{
    [Header("Custom Peoperty")]
    [Tooltip("When this time is reached the image will be displayed.")]
    [SerializeField] float startTime;
    [Tooltip("Once this time is reached the image will disappear.")]
    [SerializeField] float EndTime;

    Image targetImage;
    float timer = 0;
    public bool StartTimer = false;

    void Awake()
    {
        targetImage = GetComponent<Image>();
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
            }
        }
        
    }

    public void StartAction() 
    {
        StartTimer = true;
    }

}
