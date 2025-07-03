using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EButtonNums
{ 
    None = -1,
    TWO,
    THREE, 
    FOUR, 
    SIX,
    TEN,
}

public class ButtonActionManager : MonoBehaviour
{
    //[SerializeField][ReadOnly] private bool[] buttonTimeoutFlags = new bool[5];
    [SerializeField] private List<Button> buttons = new List<Button>();
    [SerializeField] private List<GameObject> buttonActionImages = new List<GameObject>();

    private static ButtonActionManager instance = null;

    // Sound Enum
    public List<ESoundType> soundTypes;

    public static ButtonActionManager Instance
    {
        get
        {
            if(null == instance)
            {
                Debug.LogError("Button Action Manager ��ü�� Scene�� �������� �ʽ��ϴ�. �߰����ּ���.");
            }
            Debug.Log("Button Action Manager ��ü�� ȣ��Ǿ����ϴ�.");
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
        buttons[(int)EButtonNums.TWO].interactable = false;
        GameObject triggerObj = buttonActionImages[(int)EButtonNums.TWO];
        triggerObj.SetActive(true);
        
    }

    public void ButtonAction_Three()
    {
        Debug.Log("Call Button THREE");
        buttons[(int)EButtonNums.THREE].interactable = false;
        GameObject triggerObj = buttonActionImages[(int)EButtonNums.THREE];
        triggerObj.SetActive(true);
    }

    public void ButtonAction_Four()
    {
        Debug.Log("Call Button FOUR");
        buttons[(int)EButtonNums.FOUR].interactable = false;
        GameObject triggerObj = buttonActionImages[(int)EButtonNums.FOUR];
        triggerObj.SetActive(true);
    }

    public void ButtonAction_Six()
    {
        Debug.Log("Call Button SIX");
        buttons[(int)EButtonNums.SIX].interactable = false;
        GameObject triggerObj = buttonActionImages[(int)EButtonNums.SIX];
        triggerObj.SetActive(true);
    }

    public void ButtonAction_Ten()
    {
        Debug.Log("Call Button TEN");
        buttons[(int)EButtonNums.TEN].interactable = false;
        GameObject triggerObj = buttonActionImages[(int)EButtonNums.TEN];
        triggerObj.SetActive(true);
    }

    public void SetActive(EButtonNums buttonNum)
    {
        buttons[(int)buttonNum].interactable = true;
    }

    /*public bool GetTimeout(EButtonNums buttonNum)
    {
        return buttonTimeoutFlags[(int)buttonNum];
    }

    public void SetTimeout(EButtonNums buttonNum, bool flag)
    {
        buttonTimeoutFlags[(int)buttonNum] = flag;
    }*/
}
