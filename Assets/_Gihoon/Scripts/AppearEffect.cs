using System.Collections;
using System.Collections.Generic;
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
            Debug.Log("Effect Target is not setting.");
            return;
        }

        target.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.0f);

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
                    float alpha = ((timer - startTime) / effectDuration);
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

        Debug.Log("Apper Effect Reset");
        timer = 0.0f;
        target.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        bFinish = false;
        //ButtonActionManager.Instance.SetTimeout(buttonNum, false);
    }

    private void OnDisable()
    {
        Reset();
    }
}
