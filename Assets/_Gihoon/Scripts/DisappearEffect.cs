using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;


public class DisappearEffect : MonoBehaviour
{
    [Header("Properties")]
    [Tooltip("이펙트 재생 시간 ")]public float effectDuration = 1.0f;
    [ReadOnly][Tooltip("이펙트 경과 시간")] public float effectTimer = 0.0f;
    private GameObject target = null;
    private float lifeTimer = 0.0f;
    [Tooltip("이펙트 시작 시간")] public float startTime = 0.0f;

    void Start()
    {
        target = gameObject;
        if(null == target)
        {
            Debug.Log("Effect Target is not setting.");
            return;
        }
    }

    void Update()
    {
        lifeTimer += Time.deltaTime;

        if (lifeTimer >= startTime)
        {
            effectTimer += Time.deltaTime;

            if (effectTimer <= effectDuration)
            {
                // alpha controller
                float alpha = 1.0f - (effectTimer / effectDuration);
                target.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, alpha);
            }
            else
            {
                effectTimer = 0.0f;
                this.enabled = false;
            }
        }
    }
}
