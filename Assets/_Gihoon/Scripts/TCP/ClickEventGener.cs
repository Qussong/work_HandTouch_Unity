using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickEventGen : MonoBehaviour
{
    public static void ClickAt(Vector2 screenPos)
    {
        //Debug.Log(EventSystem.current != null ? "EventSystem is active" : "EventSystem is NULL");

        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = screenPos
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        if(0 < results.Count)
        {
            foreach (RaycastResult result in results)
            {
                // Ŭ���̺�Ʈ�� �߻��� UI ������Ʈ�� Button ���� Ȯ��
                GameObject clickObj = result.gameObject;
                if(null != clickObj.GetComponent<Button>())
                {
                    ExecuteEvents.Execute(clickObj, pointerData, ExecuteEvents.pointerClickHandler);
                }

                //Debug.Log($"{clickObj.transform.name}");
            }
        }
    }
}
