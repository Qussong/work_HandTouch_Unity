using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BiggerAndRemoveScript : MonoBehaviour
{
    [Header("Custom Property")]
    [SerializeField] float maxScale;
    [SerializeField] float scaleChangeSpeed;
    [SerializeField] float RemoveSpeed;

    Image targetImage;
    Color color;
    float initialScale;
    float Timer;

    void Start()
    {
        targetImage = GetComponent<Image>();
        initialScale = targetImage.transform.localScale.x;
        color = targetImage.color;
    }


    void Update()
    {
        if (targetImage.enabled)
        {
            Timer += Time.deltaTime;

            float scale = Mathf.Lerp(initialScale, maxScale, Timer * scaleChangeSpeed);
            color.a -= Timer * RemoveSpeed;
            transform.localScale = new Vector3(scale, scale, scale);
            targetImage.color = color;
        }
        else
        {
            ResetData();
        }
    }

    void ResetData()
    {
        transform.localScale = new Vector3(initialScale, initialScale, initialScale);
        color.a = 1;
        Timer = 0;
        targetImage.color = color;
    }
}
