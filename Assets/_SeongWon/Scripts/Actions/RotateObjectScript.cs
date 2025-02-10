using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateObjectScript : MonoBehaviour
{
    [Header("Custom Property")]
    [SerializeField] float RotateSpeed;
    [SerializeField] bool isUseParentPivot;

    Image targetImage;
    Vector3 initialPosition;
    float initialZRotation;

    void Awake()
    {
        targetImage = GetComponent<Image>();
        initialZRotation = transform.eulerAngles.z;
        initialPosition = transform.position;
    }

    void Update()
    {
        if (targetImage.enabled)
        {
            float deltaAngle = (Time.deltaTime * RotateSpeed);

            if (isUseParentPivot)
                transform.RotateAround(transform.parent.position, Vector3.back, deltaAngle);
            else
                transform.Rotate(Vector3.back, deltaAngle);
        }
        else
        {
            ResetRotate();
        }
    }

    void ResetRotate() 
    {
        transform.rotation = Quaternion.Euler(0, 0, initialZRotation);
        transform.position = initialPosition;
    }
}
