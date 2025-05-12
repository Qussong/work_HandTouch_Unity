using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;

//using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;


public class DisappearEffect : MonoBehaviour, GHScriptLayout
{
    [Header("Properties")]
    [Tooltip("����Ʈ ��� �ð� ")]public float effectDuration = 1.0f;
    [ReadOnly][Tooltip("����Ʈ ��� �ð�")] public float timer = 0.0f;
    private GameObject target = null;
    [Tooltip("����Ʈ ���� �ð�")] public float startTime = 0.0f;

    private Color startColor = Color.black;

    bool bFinish = false;
    //EButtonNums buttonNum = EButtonNums.None;

    void Start()
    {
        target = gameObject;
        if(null == target)
        {
            Debug.Log("Effect Target is not setting.");
            return;
        }

        Image img = target.GetComponent<Image>();
        TextMeshProUGUI txt = target.GetComponentInChildren<TextMeshProUGUI>();

        if (null != img)
        {
            startColor = img.color;
        }
        else if (null != txt)
        {
            startColor = txt.color;
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

                    float alpha = 1.0f - ((timer - startTime) / effectDuration);
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

        timer = 0.0f;
        //target.GetComponent<Image>().color = startColor;
        bFinish = false;
        //ButtonActionManager.Instance.SetTimeout(buttonNum, false);
    }

    private void OnDisable()
    {
        Reset();
    }
}
