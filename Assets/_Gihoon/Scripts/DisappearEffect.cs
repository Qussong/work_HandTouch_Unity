using System.Collections;
using System.Collections.Generic;
using System.Threading;
//using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;


public class DisappearEffect : MonoBehaviour, GHScriptLayout
{
    [Header("Properties")]
    [Tooltip("이펙트 재생 시간 ")]public float effectDuration = 1.0f;
    [ReadOnly][Tooltip("이펙트 경과 시간")] public float timer = 0.0f;
    private GameObject target = null;
    [Tooltip("이펙트 시작 시간")] public float startTime = 0.0f;

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

        startColor = target.GetComponent<Image>().color;

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
                    float alpha = 1.0f - ((timer - startTime) / effectDuration);
                    target.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, alpha);
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

        Debug.Log("Disappear Effect Reset");
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
