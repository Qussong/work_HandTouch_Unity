using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class DisappearEffect : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] public GameObject target;
    [SerializeField] public float effectDuration;
    [SerializeField] public float effectTimer;

    void Start()
    {
        target = gameObject;
        if(null == target)
        {
            Debug.Log("Effect Target is not setting.");
            return;
        }

        effectDuration = 2.0f;
    }

    void Update()
    {
        effectTimer += Time.deltaTime;

        if(effectTimer <= effectDuration)
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
