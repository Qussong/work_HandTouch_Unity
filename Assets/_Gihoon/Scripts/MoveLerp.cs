using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLerp : MonoBehaviour
{
    [Header("Properties")]
    private RectTransform rectTransform = null;
    private float time = 0.0f;
    public Vector3 destination = Vector3.zero;
    public float speed = 0.0f;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        if(null == rectTransform)
        {
            Debug.Log("RectTransform is not setting.");
            return;
        }
    }

    void Update()
    {
        time += Time.deltaTime * speed;
        rectTransform.localPosition = Vector3.Lerp(rectTransform.localPosition, destination, time);

    }
}
