using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MoveToDestinationScript : MonoBehaviour
{
    [Header("Car Settings")]
    [SerializeField] RectTransform carTransform;
    [SerializeField] float moveSpeed = 1.0f;
    [SerializeField] float rotationSpeed = 10f;
    public float delay;
    public int index_WaitPoint;

    [Header("Waypoints")]
    [SerializeField] public RectTransform[] wayPointTransforms;

    [Header("etc")]
    [SerializeField] public EditAlphaValue editAlphaValue;
    List<Vector3> waypoints = new List<Vector3>();

    [SerializeField] float MoveTime = 1.0f;
    [SerializeField] public int EndIndex;
    [SerializeField] int CurrentIndex = 0;
    public bool IsStart = false;

    float MoveTimer;
    float delayTimer;
    bool dontMove = false;

    void Start()
    {
        Debug.Log("Start");
        carTransform = GetComponent<RectTransform>();
        editAlphaValue = GetComponent<EditAlphaValue>();

        foreach (var wp in wayPointTransforms)
        {
            wp.SetParent(carTransform.parent);
            waypoints.Add(wp.localPosition);
        }
    }

    void Update()
    {
        if (!IsStart)
            return;

        if (dontMove) 
            return;

        MoveTimer += Time.deltaTime;

        if (MoveTimer > MoveTime)
        {
            MoveNextPoint();
            MoveTimer = 0;
        }
    }

    public void MoveNextPoint()
    {
        if (CurrentIndex >= EndIndex)
        {
            dontMove = true;
            return;
        }

        Vector3 targetPosition = waypoints[CurrentIndex];

        if (Vector3.Distance(carTransform.localPosition, targetPosition) > 0.5f)
        {
            carTransform.localPosition = Vector3.MoveTowards(carTransform.localPosition, targetPosition, moveSpeed * Time.deltaTime);

            Vector3 direction = targetPosition - carTransform.localPosition;
            if (direction != Vector3.zero)
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
                carTransform.rotation = Quaternion.RotateTowards(carTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime * 100);
            }
        }
        else
        {
            carTransform.localPosition = targetPosition;
            CurrentIndex++;

            if (delay > 0)
            {
                dontMove = true;
            }
        }

    }

    public void EnableEditAlphaValue() 
    {
        editAlphaValue.enabled = true;
    }

    public void DiableEditAlphaValue()
    {
        editAlphaValue.enabled = false;
    }
}


