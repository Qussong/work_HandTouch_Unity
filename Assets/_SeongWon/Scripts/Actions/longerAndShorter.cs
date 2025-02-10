using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class longerAndShorter : MonoBehaviour
{
    [Header("Custom Property")]
    [SerializeField] float maxHeight;
    [SerializeField] float minHeight;
    [SerializeField] float speed;

    Image targetImage;
    RectTransform barRectTransform;
    Vector2 initialzePosition;
    Vector2 initialzeHeight;

    void Awake()
    {
        barRectTransform = GetComponent<RectTransform>();
        targetImage = GetComponent<Image>();
        initialzeHeight = barRectTransform.sizeDelta;
        initialzePosition = barRectTransform.anchoredPosition;
    }

    void Update()
    {
        if (targetImage.enabled)
        {
            float Height = Mathf.Lerp(minHeight, maxHeight, Mathf.PingPong(Time.time * speed, 1));
            barRectTransform.sizeDelta = new Vector2(initialzeHeight.x, Height);
        }
        else 
        {
            ResetData();
        }
    }

    void ResetData() 
    {
        barRectTransform.sizeDelta = initialzeHeight;
        barRectTransform.anchoredPosition = initialzePosition;
    }
}
