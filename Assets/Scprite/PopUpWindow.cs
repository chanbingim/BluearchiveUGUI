using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpWindowBase : MonoBehaviour
{
    public bool useBlurimage;
    public GameObject popupAnimation;
    RectTransform popupRectTrans;

    public void Initialized()
    {
        popupRectTrans = GetComponent<RectTransform>();
        popupRectTrans.localPosition = Vector3.zero;

        gameObject.SetActive(true);

        if(!popupAnimation.activeSelf)
        {
            popupAnimation.SetActive(true);
        }
        popupAnimation.GetComponent<CustomUIAnimationBase>().Play();
    }
    public void ClosePopUpWindow()
    {
        if(gameObject.activeSelf)
            gameObject.SetActive(false);
    }





}
