using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MoveSmoothDamp : MonoBehaviour, GHScriptLayout
{
    [Header("Properties")]
    private RectTransform rectTransform;
    public Vector3 destination = Vector3.zero;
    [ReadOnly] public Vector3 speed = Vector3.zero;
    public float smoothTime = 1f;
    public float startTime = 0.0f;
    [ReadOnly] public float timer = 0.0f;
    private Vector3 startPoint = Vector3.zero;

    bool bFinish = false;
    //EButtonNums buttonNum = EButtonNums.None;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        if (null == rectTransform)
        {
            Debug.Log("Rect Transform is not settings.");
            return;
        }

        startPoint = rectTransform.localPosition;
        //buttonNum = GetComponentInParent<ButtonActionScript>().buttonNum;
    }

    void Update()
    {
        if (bFinish == false)
        {
            timer += Time.deltaTime;
            if (timer > startTime)
            {
                rectTransform.localPosition = Vector3.SmoothDamp(rectTransform.localPosition, destination, ref speed, smoothTime);
            }

            if (Vector3.Distance(rectTransform.localPosition, destination) < 1.0f)
            {
                bFinish = true;
            }
        }
    }

    public void Reset()
    {
        if (rectTransform == null) return;

        Debug.Log("Move Smooth Damp Reset");
        timer = 0.0f;
        rectTransform.localPosition = startPoint;
        bFinish = false;
        //ButtonActionManager.Instance.SetTimeout(buttonNum, false);
    }

    private void OnDisable()
    {
        Reset();
    }
}
