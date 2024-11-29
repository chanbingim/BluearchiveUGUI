using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ReSizeUI : MonoBehaviour
{
    public enum ScreenSizeType
    {
        FullScreen,
        Fullwidth,
        Fullheight,
        Custom
    };

    public ScreenSizeType GUIscreenSizeType;

    Text _WidgetText;
    public Vector2 PaddingPos;

    // Start is called before the first frame update
    void Start()
    {
        if(GUIscreenSizeType < ScreenSizeType.Custom)
            ReSizeBackGround();

        if(null !=_WidgetText)
        {
            BestFitTextSize();
        }
    }

    private void ReSizeBackGround()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        switch (GUIscreenSizeType)
        {
            case ScreenSizeType.FullScreen:
                rectTransform.sizeDelta = new Vector2(Screen.width + PaddingPos.x, Screen.height + PaddingPos.y);
                break;
            case ScreenSizeType.Fullwidth:
                rectTransform.sizeDelta = new Vector2(Screen.width + PaddingPos.x, rectTransform.sizeDelta.y + PaddingPos.y);
                break;
            case ScreenSizeType.Fullheight:
                rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x + PaddingPos.x, Screen.height + PaddingPos.y);
                break;
        }
    }

    private void BestFitTextSize()
    {
        
    }
}
