using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSmoothDamp : MonoBehaviour
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

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        if (null == rectTransform)
        {
            Debug.Log("Rect Transform is not settings.");
            return;
        }

        startPoint = rectTransform.localPosition;
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

    private void Reset()
    {
        rectTransform.localPosition = startPoint;
        timer = 0.0f;
    }
}
