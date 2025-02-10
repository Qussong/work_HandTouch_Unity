using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditAlphaValue : MonoBehaviour
{
    [Header("Custom Property")]
    [SerializeField] float AlphaValueChangeSpeed;
    [SerializeField] bool IsLoop = false;
    [SerializeField] public bool IsReverse = false;

    Image targetImage;
    Color color;
    bool increasingAlpha; 

    void Awake()
    {
        targetImage = GetComponent<Image>();
        color = targetImage.color;

        if (IsReverse)
        {
            color.a = 1f;
            increasingAlpha = false;
        }
        else
        {
            color.a = 0;
            increasingAlpha = false;
        }

        targetImage.color = color;
    }

    void Update()
    {
        if (!targetImage.enabled)
        {
            ResetAlpha();
            return;
        }

        ChangeAlphaValue();
    }

    void ChangeAlphaValue()
    {
        if (IsLoop)
        {
            if (IsReverse)
            {
                if (increasingAlpha)
                {
                    color.a += Time.deltaTime * AlphaValueChangeSpeed;
                    if (color.a >= 1f)
                    {
                        color.a = 1f;
                        increasingAlpha = false;
                    }
                }
                else
                {
                    color.a -= Time.deltaTime * AlphaValueChangeSpeed;
                    if (color.a <= 0f)
                    {
                        color.a = 0f;
                        increasingAlpha = true;
                    }
                }
            }
            else
            {
                if (increasingAlpha)
                {
                    color.a += Time.deltaTime * AlphaValueChangeSpeed;
                    if (color.a >= 1f)
                    {
                        color.a = 1f;
                        increasingAlpha = false;
                    }
                }
                else
                {
                    color.a -= Time.deltaTime * AlphaValueChangeSpeed;
                    if (color.a <= 0f)
                    {
                        color.a = 0f;
                        increasingAlpha = true;
                    }
                }
            }
        }
        else
        {
            if (IsReverse)
            {
                if (color.a > 0f)
                {
                    color.a -= Time.deltaTime * AlphaValueChangeSpeed;
                    color.a = Mathf.Clamp01(color.a);
                }
            }
            else 
            {
                if (color.a < 1f)
                {
                    color.a += Time.deltaTime * AlphaValueChangeSpeed;
                    color.a = Mathf.Clamp01(color.a);
                }
            }
        }

        targetImage.color = color;
    }

    public void ResetAlpha()
    {
        if (IsReverse)
            color.a = 1f;
        else
            color.a = 0;

        targetImage.color = color;
    }
}
