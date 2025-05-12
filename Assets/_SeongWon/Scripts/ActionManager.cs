using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionManager : MonoBehaviour
{

    [SerializeField] bool IsMain = false;
    [SerializeField] ActionScriptInterface[] actionScripts;
    [SerializeField] Button button;
    [SerializeField] float BlockButtonTime;

    float BlockButtonTimer = 0.0f;
    bool IsPlay = false;
    void Start()
    {
        actionScripts = GetComponentsInChildren<ActionScriptInterface>();
    }

    void OnEnable()
    {
        if (IsMain) 
        {
            for (int i = 0; i < actionScripts.Length; i++)
            {
                actionScripts[i].StartAction();
            }
        }
    }

    void OnDisable()
    {
        if (IsMain) 
        {
            for (int i = 0; i < actionScripts.Length; i++)
            {
                actionScripts[i].StartAction();
            }
        }
    }


    void Update()
    {
        if (BlockButtonTime <= 0)
            return;

        if (!button.enabled) 
        {
            BlockButtonTimer += Time.deltaTime;

            if (BlockButtonTimer > BlockButtonTime) 
            {
                button.enabled = true;
                BlockButtonTimer = 0.0f;
            }
        }
    }

    public void OnClickedButton() 
    {

        for (int i = 0; i < actionScripts.Length; i++)
        {
            actionScripts[i].StartAction();
        }

        button.enabled = false;
        
    }
}
