using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonActionManager : MonoBehaviour
{
    private static ButtonActionManager instance = null;

    public static ButtonActionManager Instance
    {
        get
        {
            if(null == instance)
            {
                Debug.LogError("Button Action Manager 객체가 Scene에 존재하지 않습니다. 추가해주세요.");
            }
            Debug.Log("Button Action Manager 객체가 호출되었습니다.");
            return instance;
        }
    }

    protected void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("Set Button Action Manager");
        }
        else
        {
            Destroy(instance);
            Debug.Log("Destroy Button Action Manager");
        }
    }

    public void ButtonAction_Two()
    {
        Debug.Log("Call Button TWO");
    }

    public void ButtonAction_Three()
    {
        Debug.Log("Call Button THREE");
    }

    public void ButtonAction_Four()
    {
        Debug.Log("Call Button FOUR");
    }

    public void ButtonAction_Six()
    {
        Debug.Log("Call Button SIX");
    }

    public void ButtonAction_Ten()
    {
        Debug.Log("Call Button TEN");
    }

}
