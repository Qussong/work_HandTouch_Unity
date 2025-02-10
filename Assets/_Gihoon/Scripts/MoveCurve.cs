using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCurve : MonoBehaviour
{
    [Header("Properties")]
    public List<Vector3> points = new List<Vector3>();
    [ReadOnly][Tooltip("시작점")] public Vector3 startPoint = Vector3.zero;    // 시작점
    [ReadOnly][Tooltip("끝점")] public Vector3 endPoint = Vector3.zero;        // 끝점
    [Tooltip("재생 시간")]public float duration = 1f;
    [ReadOnly][Tooltip("진행도")] public float ratio = 0.0f;
    [ReadOnly][Tooltip("경과 시간")] public float time = 0.0f;
    private GameObject target = null;
    bool bStartCondition = true;

    void Start()
    {
        target = gameObject;
        if(null == target)
        {
            Debug.Log("Target is not setting.");
            bStartCondition = false;
            return;
        }

        if(points.Count < 2)
        {
            Debug.LogError("Points is not setting.");
            bStartCondition = false;
            return;
        }

        startPoint = points[0];
        endPoint = points[points.Count - 1];
    }
    
    void Update()
    {
        if (bStartCondition == false) return;

        time += Time.deltaTime;
        ratio = (time / duration); // 속도 조절
        target.GetComponent<RectTransform>().localPosition = BezierCurve(points, ratio);

        if(ratio >= 1.0f)
        {
            target.GetComponent<RectTransform>().localPosition = endPoint;
            this.enabled = false;
        }
    }

    // nCr 계산 함수 (조합)
    private static int BinomialCoefficient(int n, int k)
    {
        if (k == 0 || k == n) return 1;
        int result = 1;
        for (int i = 1; i <= k; i++)
        {
            result = result * (n - i + 1) / i;
        }
        return result;
    }

    // N차 베지어 곡선 계산 함수
    public static Vector3 BezierCurve(List<Vector3> points, float t)
    {
        int n = points.Count - 1; // 곡선 차수 = 제어점 개수 - 1
        Vector3 result = Vector3.zero;

        for (int i = 0; i <= n; i++)
        {
            float coefficient = BinomialCoefficient(n, i) * Mathf.Pow(1 - t, n - i) * Mathf.Pow(t, i);
            result += coefficient * points[i];
        }

        return result;
    }
}
