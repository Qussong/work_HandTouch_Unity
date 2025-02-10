using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CarManager : MonoBehaviour, ActionScriptInterface
{
    [SerializeField] CarSpawner[] carSpawners;


    void Awake()
    {

    }

    void Update()
    {

    }

    public void StartAction() 
    {
        foreach (var carSpawner in carSpawners)
        {
            carSpawner.isStart = true;
        }
    }

}


