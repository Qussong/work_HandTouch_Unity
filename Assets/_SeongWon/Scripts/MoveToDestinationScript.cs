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
    public int index_WaitPoint;

    [Header("Waypoints")]
    [SerializeField] public RectTransform[] wayPointTransforms;
    public int currentWaypointIndex;

    [Header("etc")]
    [SerializeField] public EditAlphaValue editAlphaValue;

    List<Vector2> waypoints = new List<Vector2>();

    [SerializeField] float MoveTime;
    [SerializeField] int EndIndex;
    [SerializeField] int CurrentIndex = 0;

    float MoveTimer;

    void Start()
    {
        carTransform = GetComponent<RectTransform>();
        editAlphaValue = GetComponent<EditAlphaValue>();

        foreach (var wp in wayPointTransforms)
        {
            waypoints.Add(wp.anchoredPosition);
        }
    }

    void Update()
    {
        MoveTimer += Time.deltaTime;

        if (MoveTimer > MoveTime) 
        {
            MoveNextPointCoroutine();
            MoveTimer = 0;
        }
    }

    public void MoveNextPointCoroutine()
    {
        if (CurrentIndex >= EndIndex)
        {
            Destroy(gameObject);
        }
        else 
        {
            Vector2 targetPosition = waypoints[currentWaypointIndex];

            if (Vector2.Distance(carTransform.anchoredPosition, targetPosition) > 1.0f)
            {
                carTransform.anchoredPosition = Vector2.MoveTowards(carTransform.anchoredPosition, targetPosition,
                    moveSpeed * Time.deltaTime);

                Vector2 direction = targetPosition - carTransform.anchoredPosition;

                if (direction != Vector2.zero)
                {
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
                    carTransform.rotation = Quaternion.Lerp(carTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                }
            }
            else 
            {
                carTransform.anchoredPosition = targetPosition;
                CurrentIndex++;
            }
        }
 
    }
}


