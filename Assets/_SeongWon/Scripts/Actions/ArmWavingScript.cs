using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmWavingScript : MonoBehaviour
{
    [Header("Custom Property")]
    [SerializeField] float wavingAngle;
    [SerializeField] float wavingSpeed;
    [SerializeField] float delay;
    [SerializeField] Transform jointRectTransform;
    [SerializeField] Image EventImage;

    Image targetImage;
    bool dontMove = false;
    Vector3 initialzePosition;
    float initialZRotation;
    float currentAngle = 0f;
    float delayTimer;
    float deltaTimer;

    void Awake()
    {
        targetImage = GetComponent<Image>();
        initialZRotation = transform.eulerAngles.z;
        initialzePosition = transform.position;
    }

    void Update()
    {
        if (!targetImage.enabled)
        {
            ResetRotate();
            return;
        }

        float wavingPersent;
        float angleOffset;

        if (dontMove) 
        {
            EventImage.enabled = true;

            delayTimer += Time.deltaTime;

            if (delayTimer > delay) 
            {
                delayTimer = 0f;
                deltaTimer += 0.3f;
                dontMove = false;
                EventImage.enabled = false;
            }

            return;
        }

        deltaTimer += Time.deltaTime;
        wavingPersent = Mathf.Sin(deltaTimer * wavingSpeed);
        angleOffset = wavingAngle * wavingPersent;
        float deltaAngle = angleOffset - currentAngle;

        transform.RotateAround(jointRectTransform.position, Vector3.forward, deltaAngle);
        currentAngle = angleOffset;

        if (delay > 0 && wavingPersent >= 0.99f)
        {
            dontMove = true;
        }
        
    }


    void ResetRotate() 
    {
        transform.rotation = Quaternion.Euler(0, 0, initialZRotation);
        transform.position = initialzePosition;
    }
}


