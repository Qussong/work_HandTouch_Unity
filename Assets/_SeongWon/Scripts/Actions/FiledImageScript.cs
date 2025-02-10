using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FiledImageScript : MonoBehaviour
{
    [Header("Custom Property")]
    [SerializeField] float filedSpeed;
    [SerializeField] float delay;
    [SerializeField] bool isLoop;

    Image targetImage;
    bool dontAction = false;
    float initialFilledValue;
    float delayTimer;

    void Awake()
    {
        targetImage = GetComponent<Image>();
        initialFilledValue = 0.0f;
        targetImage.fillAmount = initialFilledValue;
    }

    void Update()
    {
        if (!targetImage.enabled)
        {
            Reset();
            return;
        }

        if (dontAction)
        {

            delayTimer += Time.deltaTime;

            if (delayTimer > delay)
            {
                delayTimer = 0f;
                dontAction = false;
            }

            return;
        }

        targetImage.fillAmount += Time.deltaTime * filedSpeed;

        if (targetImage.fillAmount >= 1.0f)
        {
            targetImage.fillAmount = 1.0f;

            if (delay > 1)
            {
                dontAction = true;
            }
        }

    }


    void Reset()
    {
        initialFilledValue = 0.0f;
        targetImage.fillAmount = initialFilledValue;
        delayTimer = 0.0f;
    }
}
