using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotationChildPivot : MonoBehaviour
{
    [Header("Properties")]
    public float startTime = 0.0f;
    [ReadOnly] public float timer = 0.0f;
    public Image pivotChild = null;
    public float rotateAngle = 0.0f;
    public float rotateSpeed = 1.0f;

    private Vector3 directionVec = Vector3.forward;

    void Start()
    {
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > startTime)
        {
            if (pivotChild == null)
            {
                GetComponent<RectTransform>().Rotate(directionVec, rotateAngle * Time.deltaTime * rotateSpeed);
            }
            else
            {
                GetComponent<RectTransform>().RotateAround(pivotChild.transform.position, directionVec, rotateAngle * Time.deltaTime * rotateSpeed);
            }

            if(timer > (startTime + 1.0f))
            {
                this.enabled = false;
            }
        }
    }
}
