using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWayPoint : MonoBehaviour
{
    [Header("Properties")]
    private RectTransform rectTransform = null;
    private int currentIdx = 0;
    public Vector3[] wayPoints;
    public float speed = 0.0f;
    public bool isLoop = true;


    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        if(rectTransform == null)
        {
            Debug.Log("RectTransform is not setting.");
            return;
        }

        speed = 100.0f;
    }

    void Update()
    {
        if(wayPoints.Length == 0)
        {
            Debug.Log("Way Points is empty");
            return;
        }

        rectTransform.localPosition = Vector3.MoveTowards(rectTransform.localPosition, wayPoints[currentIdx], speed * Time.deltaTime);

        if(Vector3.Distance(rectTransform.localPosition, wayPoints[currentIdx]) < 0.1f)
        {
            if(isLoop == true)
            {
                currentIdx = (currentIdx + 1) % wayPoints.Length;
            }
            else
            {
                currentIdx = currentIdx < wayPoints.Length - 1 ? ++currentIdx : currentIdx;
            }
        }
    }
}
