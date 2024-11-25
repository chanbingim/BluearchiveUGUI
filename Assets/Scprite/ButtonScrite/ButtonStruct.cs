using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void ButtonEventdele();

public enum EEventType
{
    NextStageButton,
    ClickEventButton,
    ToggleButton,
    DynamicButton,
};

[Serializable]
public struct ToggleButData
{
    public GameObject ApplayObj;
    public List<Sprite> ToggleImage;
    public bool bIsToggle;

    public ButtonEventdele ButtonClickedEvent;
}

[Serializable]
public struct ClickButData
{
    public GameObject ApplayObj;
    public GameObject ShowPopUpUI;
    public ButtonEventdele ButtonClickedEvent;
}

[Serializable]
public struct DynamicButData
{
    public GameObject ApplayObj;
    public float ShowTimeRate;
    [HideInInspector] public float EndTime;
    public bool bIsActive;
    public ButtonEventdele ButtonClickedEvent;
}