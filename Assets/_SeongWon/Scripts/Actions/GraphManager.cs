using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphManager : MonoBehaviour
{
    Image OwnerImage;
    Image[] BarGraphs;

    bool isEnableImages = false;
    

    void Start()
    {
        BarGraphs = GetComponentsInChildren<Image>();
        OwnerImage = GetComponent<Image>();
    }

    void Update()
    {
        if (OwnerImage.enabled)
        {
            EnableImages();
        }
        else 
        {
            DisableImages();
        }
    }

    void EnableImages() 
    {
        if (isEnableImages)
            return;

        foreach (var image in BarGraphs)
        {
            if (!image.enabled)
            {
                image.enabled = true;
            }
        }

        isEnableImages = true;
    }

    void DisableImages() 
    {
        if(!isEnableImages)
            return;

        foreach (var image in BarGraphs)
        {
            if (image.enabled)
            {
                image.enabled = false;
            }
        }

        isEnableImages = false;
    }
}
