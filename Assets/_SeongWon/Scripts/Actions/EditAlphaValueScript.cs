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
    [SerializeField] float Delay;
    [SerializeField] float StartTime;

    Image targetImage;
    Color color;
    bool increasingAlpha;
    bool DontMove = false;
    float DelayTimer = 0;

    private void OnDisable()
    {
        ResetAlpha();
    }

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

        if (DontMove) 
        {
            DelayTimer += Time.deltaTime;

            if (DelayTimer > Delay)
            {
                DelayTimer = 0;
                DontMove = false;
            }
            
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

                        if (Delay > 0) 
                        {
                            DontMove = true;
                        }
                    }
                }
                else
                {
                    color.a -= Time.deltaTime * AlphaValueChangeSpeed;
                    if (color.a <= 0f)
                    {
                        color.a = 0f;
                        increasingAlpha = true;
                        //AudioManager.instance.PlaySound(ESoundType.Cannon);

                        if (Delay > 0)
                        {
                            DontMove = true;
                        }
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

                        if (Delay > 0)
                        {
                            DontMove = true;
                        }
                    }
                }
                else
                {
                    color.a -= Time.deltaTime * AlphaValueChangeSpeed;
                    if (color.a <= 0f)
                    {
                        color.a = 0f;
                        increasingAlpha = true;

                        if (Delay > 0)
                        {
                            DontMove = true;
                        }
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

                    if (Delay > 0 && color.a >= 1.0)
                    {
                        DontMove = true;
                    }
                }
            }
            else 
            {
                if (color.a < 1f)
                {
                    color.a += Time.deltaTime * AlphaValueChangeSpeed;
                    color.a = Mathf.Clamp01(color.a);
                }

                if (Delay > 0 && color.a <= 0.0f)
                {
                    DontMove = true;
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
