using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CarManager : MonoBehaviour, ActionScriptInterface
{
    [SerializeField] CarSpawner[] carSpawners;
    [SerializeField] ShowImageScript showImageScript;
    [SerializeField] float RemoveTime;


    bool IsEnd;
    float RemoveTimer;


    void Awake()
    {
        showImageScript = GetComponent<ShowImageScript>();
    }

    void Update()
    {
        if (!showImageScript.StartTimer && IsEnd) 
        {
            EndAction();
            IsEnd = false;
            return;
        }

        if (showImageScript.StartTimer && RemoveTime > 0) 
        {
            RemoveTimer += Time.deltaTime;

            if (RemoveTimer > RemoveTime)
            {
                EnableChangeAlphaValue();
                return;

            }
        }
    }

    public void StartAction() 
    {
        if (carSpawners.Length <= 0)
            return;

        foreach (var carSpawner in carSpawners)
        {
            carSpawner.isStart = true;
        }

        IsEnd = true;
    }

    public void EnableChangeAlphaValue() 
    {
        if (carSpawners.Length <= 0)
            return;

        foreach (var carSpawner in carSpawners)
        {
            carSpawner.EnableChangeAlphaValue();
        }
    }

    public void EndAction() 
    {
        if (carSpawners.Length <= 0)
            return;

        foreach (var carSpawner in carSpawners)
        {
            carSpawner.DestoryAllCar(); ;
        }

        RemoveTimer = 0;
    }

}


