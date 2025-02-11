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
    [SerializeField] bool IsLoop;

    Image targetImage;
    RectTransform barRectTransform;
    Vector2 initialzePosition;
    Vector2 initialzeHeight;
    bool DontMove;
    float t;

    void Awake()
    {
        barRectTransform = GetComponent<RectTransform>();
        targetImage = GetComponent<Image>();
        initialzeHeight = barRectTransform.sizeDelta;
        initialzePosition = barRectTransform.anchoredPosition;
    }

    void Update()
    {
        if (IsLoop) 
        {
            DontMove = false;
        }


        if (targetImage.enabled && !DontMove)
        {
            if (IsLoop)
            {
                t = Mathf.PingPong(Time.time * speed, 1);
            }
            else 
            {
                t += Time.deltaTime * speed;
            }

            float Height = Mathf.Lerp(minHeight, maxHeight, t);
            Debug.Log(Height);
            barRectTransform.sizeDelta = new Vector2(barRectTransform.sizeDelta.x, Height);

            if (barRectTransform.sizeDelta.y > maxHeight) 
            {
                DontMove = true;
            }
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
