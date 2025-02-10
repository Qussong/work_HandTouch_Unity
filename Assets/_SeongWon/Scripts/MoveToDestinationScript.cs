using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] RectTransform[] wayPointTransforms;
    public int currentWaypointIndex;
    

    [Header("etc")]
    [SerializeField] EditAlphaValue editAlphaValue;
    [SerializeField] CarManager carManager;

    Image targetImage;
    List<Vector2> waypoints = new List<Vector2>();
    Vector2 initialPosition;

    bool isMoving = false;

    void Start()
    {
        targetImage = GetComponent<Image>();
        carTransform = GetComponent<RectTransform>();
        editAlphaValue = GetComponent<EditAlphaValue>();
        initialPosition = carTransform.anchoredPosition;

        foreach (var wp in wayPointTransforms)
        {
            waypoints.Add(wp.anchoredPosition);
        }
    }

    public IEnumerator MoveNextPointCoroutine()
    {
        isMoving = true;

        if (currentWaypointIndex == 0)
        {
            if (targetImage != null)
                targetImage.enabled = true;
        }

        if (currentWaypointIndex > index_WaitPoint)
        {
            editAlphaValue.enabled = true;

            while (currentWaypointIndex < waypoints.Count)
            {

                Vector2 targetPosition = waypoints[currentWaypointIndex];
                currentWaypointIndex++;


                while (Vector2.Distance(carTransform.anchoredPosition, targetPosition) > 1.0f)
                {
                    carTransform.anchoredPosition = Vector2.MoveTowards(
                        carTransform.anchoredPosition,
                        targetPosition,
                        moveSpeed * Time.deltaTime
                    );

                    Vector2 direction = targetPosition - carTransform.anchoredPosition;
                    if (direction != Vector2.zero)
                    {
                        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
                        carTransform.rotation = Quaternion.Lerp(carTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                    }
                    yield return null;
                }

                carTransform.anchoredPosition = targetPosition;
            }

            carTransform.anchoredPosition = initialPosition;
            editAlphaValue.ResetAlpha();
            editAlphaValue.enabled = false;
            currentWaypointIndex = 0;
        }
        else
        {
            Vector2 targetPosition = waypoints[currentWaypointIndex];
            currentWaypointIndex++;

            while (Vector2.Distance(carTransform.anchoredPosition, targetPosition) > 1.0f)
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

                yield return null;
            }

            carTransform.anchoredPosition = targetPosition;
        }

        isMoving = false;
    }
}


