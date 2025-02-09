using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCurve : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] public Vector3 startPoint = new Vector3(0, 0, 0);          // 시작점
    [SerializeField] public Vector3 ControllPoint1 = new Vector3(20, 30, 0);    // 제어점 1
    [SerializeField] public Vector3 ControllPoint2 = new Vector3(40, -20, 0);   // 제어점 2
    [SerializeField] public Vector3 endPoint = new Vector3(60, 20, 0);          // 끝점

    private float time = 0.0f;
    private RectTransform rectTransform = null;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        if(null == rectTransform)
        {
            Debug.Log("Rect Transform is not setting.");
        }
    }
    
    void Update()
    {
        time += Time.deltaTime * 0.5f; // 속도 조절
        rectTransform.localPosition = BezierCurve(startPoint, ControllPoint1, ControllPoint2, endPoint, time);

        if(Vector3.Distance(rectTransform.localPosition, endPoint) < 0.1f)
        {
            this.enabled = false;
            return;
        }
    }

    // Bezier 곡선 계산 함수
    Vector3 BezierCurve(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector3 result =
            uuu * p0 +          // (1-t)^3 * P0
            3 * uu * t * p1 +   // 3(1-t)^2 * t * P1
            3 * u * tt * p2 +   // 3(1-t) * t^2 * P2
            ttt * p3;           // t^3 * P3

        return result;
    }
}
