using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotationLoop : MonoBehaviour, GHScriptLayout
{
    [Header("Properties")]
    public float startTime = 0.0f;
    [ReadOnly] public float timer = 0.0f;
    public Image pivotChild = null;
    public float rotateAngle = 0.0f;
    public float pendulumTime = 0.0f;
    [ReadOnly] public float pendulumTimer = 0.0f;

    Vector3 direction = Vector3.back;

    bool bFinish = false;

    void Start()
    {
        
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(timer > startTime)
        {
            if(pivotChild == null)
            {
                GetComponent<RectTransform>().Rotate(Vector3.forward, rotateAngle * Time.deltaTime);
            }
            else
            {
                pendulumTimer += Time.deltaTime;
                if(pendulumTimer > pendulumTime)
                {
                    direction *= -1;
                    pendulumTimer = 0.0f;
                }
                GetComponent<RectTransform>().RotateAround(pivotChild.transform.position, direction, rotateAngle * Time.deltaTime);
            }
        }
    }

    public void Reset()
    {
        timer = 0.0f;
        bFinish = true;
    }

    private void OnDisable()
    {
        Reset();
    }
}
