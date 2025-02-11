using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EButtonNums
{ 
    TWO = 0,
    THREE, 
    FOUR, 
    SIX,
    TEN,
}

public class ButtonActionManager : MonoBehaviour
{
    [SerializeField][ReadOnly] private bool[] buttonActionFlags = new bool[5];
    [SerializeField] private List<GameObject> buttonActionImages = new List<GameObject>();

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
        buttonActionFlags[(int)EButtonNums.TWO] = true;        GameObject triggerObj = buttonActionImages[(int)EButtonNums.TWO];
        triggerObj.SetActive(true);
    }

    public void ButtonAction_Three()
    {
        Debug.Log("Call Button THREE");
        buttonActionFlags[(int)EButtonNums.THREE] = true;
        GameObject triggerObj = buttonActionImages[(int)EButtonNums.THREE];
        triggerObj.SetActive(true);
    }

    public void ButtonAction_Four()
    {
        Debug.Log("Call Button FOUR");
        buttonActionFlags[(int)EButtonNums.FOUR] = true;
        GameObject triggerObj = buttonActionImages[(int)EButtonNums.FOUR];
        triggerObj.SetActive(true);
    }

    public void ButtonAction_Six()
    {
        Debug.Log("Call Button SIX");
        buttonActionFlags[(int)EButtonNums.SIX] = true;
        GameObject triggerObj = buttonActionImages[(int)EButtonNums.SIX];
        triggerObj.SetActive(true);
    }

    public void ButtonAction_Ten()
    {
        Debug.Log("Call Button TEN");
        buttonActionFlags[(int)EButtonNums.TEN] = true;
        GameObject triggerObj = buttonActionImages[(int)EButtonNums.TEN];
        triggerObj.SetActive(true);
    }
}
