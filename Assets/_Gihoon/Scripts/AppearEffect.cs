using System.Collections;
using System.Collections.Generic;
using TMPro;


//using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class AppearEffect : MonoBehaviour, GHScriptLayout
{
    [Header("Properties")]
    [Tooltip("이펙트 재생 시간 ")] public float effectDuration = 1.0f;
    [ReadOnly][Tooltip("이펙트 경과 시간")] public float timer = 0.0f;
    private GameObject target = null;
    [Tooltip("이펙트 시작 시간")] public float startTime = 0.0f;

    bool bFinish = false;
    //EButtonNums buttonNum = EButtonNums.None;

    void Start()
    {
        target = gameObject;
        if (null == target)
        {
            return;
        }

        Image img = target.GetComponent<Image>();
        TextMeshProUGUI txt = target.GetComponentInChildren<TextMeshProUGUI>();

        if (null != img)
        {
            img.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        }
        else if(null != txt)
        {
            txt.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        }
        else
        {
            Debug.LogWarning("No Target - AppearEffect");
        }

        //buttonNum = GetComponentInParent<ButtonActionScript>().buttonNum;
    }

    void Update()
    {
        if (bFinish == false)
        {
            timer += Time.deltaTime;

            if (timer >= startTime)
            {
                if (timer <= (startTime + effectDuration))
                {
                    // alpha controller
                    //target.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, alpha);

                    float alpha = ((timer - startTime) / effectDuration);

                    Image img = target.GetComponent<Image>();
                    TextMeshProUGUI txt = target.GetComponentInChildren<TextMeshProUGUI>();

                    if (null != img)
                    {
                        img.color = new Color(1.0f, 1.0f, 1.0f, alpha);
                    }
                    else if (null != txt)
                    {
                        txt.color = new Color(1.0f, 1.0f, 1.0f, alpha);
                    }
                    else
                    {
                        Debug.LogWarning("No Target - AppearEffect");
                    }


                }
                else
                {
                    bFinish = true;
                }
            }
        }

    }

    public void Reset()
    {
        if (target == null) return;

        //target.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        //ButtonActionManager.Instance.SetTimeout(buttonNum, false);

        timer = 0.0f;
        Image img = target.GetComponent<Image>();
        TextMeshProUGUI txt = target.GetComponentInChildren<TextMeshProUGUI>();

        if (null != img)
        {
            img.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        }
        else if (null != txt)
        {
            txt.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        }
        else
        {
            Debug.LogWarning("No Target - AppearEffect");
        }

        bFinish = false;
    }

    private void OnDisable()
    {
        Reset();
    }
}
