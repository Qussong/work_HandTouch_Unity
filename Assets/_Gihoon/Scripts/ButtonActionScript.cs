using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonActionScript : MonoBehaviour
{
    [Header("Properties")]
    public float startTime = 0.0f;
    public float duration = 0.0f;
    [SerializeField][ReadOnly] private float timer = 0.0f;

    void Start()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > startTime)
        {
            // start logic

            if(timer > startTime + duration)
            {
                // end logic
                timer = 0.0f;
                gameObject.SetActive(false);
            }
        }
    }
}
