using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoomInOutScript : MonoBehaviour
{
    [Header("Custom Property")]
    [SerializeField] float maxScale;
    [SerializeField] float scaleChangeSpeed;

    Image targetImage;
    float initialScale;

    void Start()
    {
        targetImage = GetComponent<Image>();
        initialScale = targetImage.transform.localScale.x;
    }


    void Update()
    {
        if (targetImage.enabled == true)
        {
            float t = Mathf.PingPong(Time.time * scaleChangeSpeed, 1f);
            float scale = Mathf.Lerp(initialScale, maxScale, t);
            transform.localScale = new Vector3(scale, scale, scale);
        }
        else 
        {
            transform.localScale = new Vector3(initialScale, initialScale, initialScale);
        }
    }
}
