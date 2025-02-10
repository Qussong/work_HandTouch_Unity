using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BiggerAndRemoveScript : MonoBehaviour
{
    [Header("Custom Property")]
    [SerializeField] float maxScale;
    [SerializeField] float scaleChangeSpeed;
    [SerializeField] float Delay;
    [SerializeField] EditAlphaValue editAlphaValue;

    Image targetImage;
    float initialScale;
    float timer;
    float delayTimer;
    bool dontPlay = false;

    void Start()
    {
        targetImage = GetComponent<Image>();
        editAlphaValue = GetComponent<EditAlphaValue>();
        initialScale = targetImage.transform.localScale.x;
        delayTimer = 0.0f;
    }


    void Update()
    {
        if (dontPlay) 
        {
            delayTimer += Time.deltaTime;

            if (delayTimer > Delay) 
            {
                dontPlay = false;
            }
            return;
        }

        if (targetImage.enabled && transform.localScale.x < maxScale)
        {
            timer += Time.deltaTime;

            float scale = Mathf.Lerp(initialScale, maxScale, timer * scaleChangeSpeed);
            transform.localScale = new Vector3(scale, scale, scale);

            if (transform.localScale.x >= maxScale && Delay > 0)
            {
                dontPlay = true;
            }
            
        }
        else
        {
            ResetData();
            editAlphaValue.ResetAlpha();
        }
    }

    void ResetData()
    {
        transform.localScale = new Vector3(initialScale, initialScale, initialScale);
        timer = 0;
    }
}
