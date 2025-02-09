using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WobbleScript : MonoBehaviour
{
    [Header("Custom Property")]
    [SerializeField] float wobbleAngle;
    [SerializeField] float wobbleSpeed;

    Image targetImage;
    float initialZRotation;

    void Start()
    {
        targetImage = GetComponent<Image>();
        initialZRotation = targetImage.transform.eulerAngles.z;
    }


    void Update()
    {
        if (targetImage.enabled == true)
        {
            float angle = wobbleAngle * Mathf.Sin(Time.time * wobbleSpeed);
            transform.rotation = Quaternion.Euler(0, 0, initialZRotation + angle);
        }
        else 
        {
            transform.rotation = Quaternion.identity;
        }
    }
}
