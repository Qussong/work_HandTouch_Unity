using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSmoothDamp : MonoBehaviour
{
    [Header("Properties")]
    private RectTransform rectTransform;
    public Vector3 destination = Vector3.zero;
    //public float speed = 0.0f;
    public Vector3 speed = Vector3.zero;
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        if (null == rectTransform)
        {
            Debug.Log("Rect Transform is not settings.");
            return;
        }

        destination = new Vector3(0.0f, rectTransform.localPosition.y, rectTransform.localPosition.z);
        //speed = 100.0f;
        speed = new Vector3(100.0f, 0.0f, 0.0f);
    }

    void Update()
    {
        rectTransform.localPosition = Vector3.SmoothDamp(rectTransform.localPosition, destination, ref speed, 0.5f);
    }
}
