using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    static UIManager instance = null;
    [SerializeField] private GameObject DataContext;
    [SerializeField] private GameObject popUpBlurImage;
    [SerializeField] private List<GameObject> showPhoto_DynamicButList;
    [SerializeField] private float ShowTimeRate;

    private GameObject currentActivePopupObj;
    private List<GameObject> DynamicButList;

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public static UIManager GetInstance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            else
                return instance;
        }
    }

    private void Start()
    {
        showPhoto_DynamicButList = DataContext.GetComponent<SceneContext>().showPhto_DyamicUIList;
    }

    public void showDynamicBut()
    {
        foreach (GameObject obj in showPhoto_DynamicButList)
        {
            var but = obj.GetComponent<CustomButtonScrite>();
            if (!but.gameObject.activeSelf)
            {
                but.gameObject.SetActive(true);
            }
            else
            {
                but.AddShowEndTime();
            }

        }
    }

    public void ShowPopupWindow(GameObject obj)
    {
        var popupWindow = obj.GetComponent<PopUpWindowBase>();
        if (popupWindow.useBlurimage)
        {
            if (popUpBlurImage)
                popUpBlurImage.SetActive(true);
            else
                Debug.Log("None Find Blur");

            currentActivePopupObj = obj;
            popupWindow.Initialized();
        }
    }

    public void ClosePopUpWindow()
    {
        if (popUpBlurImage)
        {
            if (popUpBlurImage.activeSelf)
            {
                popUpBlurImage.SetActive(false);
            }
        }
        
        if(currentActivePopupObj)
        {
            if(currentActivePopupObj.activeSelf)
                currentActivePopupObj.SetActive(false);
        }
         
    }
}
