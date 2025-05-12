using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    private void Awake()
    {
        Button[] buttons = GetComponentsInChildren<Button>();

        foreach (Button b in buttons) 
        {
            b.onClick.AddListener(() => AudioManager.instance.PlaySound(ESoundType.Click));
        }
    }
}
