using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    [SerializeField] ActionScriptInterface[] actionScripts;
    void Start()
    {
        actionScripts = GetComponentsInChildren<ActionScriptInterface>();
    }

    
    void Update()
    {
        
    }

    public void OnClickedButton() 
    {

        for (int i = 0; i < actionScripts.Length; i++)
        {
            actionScripts[i].StartAction();
        }
    }
}
