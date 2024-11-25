using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;


public class ButtonEvent : Button, IPointerEnterHandler, IPointerExitHandler
{
    protected ButtonEvent()
    { }

    [Serializable]
    public class ButtonHoverEnterEvent : UnityEvent { }

    [Serializable]
    public class ButtonHoverExitEvent : UnityEvent { }

    // Event delegates triggered on click.
    [FormerlySerializedAs("HoverEnter")]
    [SerializeField]
    private ButtonHoverEnterEvent HoverEnter = new ButtonHoverEnterEvent();

    // Event delegates triggered on click.
    [FormerlySerializedAs("HoverExit")]
    [SerializeField]
    private ButtonHoverExitEvent HoverExit = new ButtonHoverExitEvent();

    public ButtonHoverEnterEvent enterEvent
    {
        get { return HoverEnter; }
        set { HoverEnter = value; }
    }

    public ButtonHoverExitEvent exitEvent
    {
        get { return HoverExit; }
        set { HoverExit = value; }
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        enterEvent.Invoke();
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        exitEvent.Invoke();
    }
}
