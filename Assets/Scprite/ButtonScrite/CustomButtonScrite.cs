using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CustomButtonScrite : MonoBehaviour
{
    [SerializeField]
    public EEventType ButtonType;

    [ShowIf("ButtonType", EEventType.NextStageButton)]
    public string nextStageName;

    [ShowIf("ButtonType", EEventType.ClickEventButton)]
    public ClickButData C_ButData;

    [ShowIf("ButtonType", EEventType.ToggleButton)]
    public ToggleButData T_ButData;

    [ShowIf("ButtonType", EEventType.DynamicButton)]
    public DynamicButData D_ButData;

    public void OncClickEvent()
    {
        if(C_ButData.ShowPopUpUI)
        {
            UIManager.GetInstance.ShowPopupWindow(C_ButData.ShowPopUpUI);
        }
    }

    public void CallStageEvent()
    {
        SceneChangeManager.GetInstance.ChangeNextScene(nextStageName);
    }
    public void OnToggleEvent()
    {
        ChangeToggleImageInChild();
        T_ButData.bIsToggle = !T_ButData.bIsToggle;
    }
    private void ChangeToggleImageInChild()
    {
        var butImage = T_ButData.ApplayObj.GetComponent<Image>();

        int index = T_ButData.bIsToggle ? 1 : 0;
        butImage.sprite = T_ButData.ToggleImage[index];
    }
    public void HiddenDynamicButtonUI()
    {
        if(gameObject.activeSelf)
            gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        if (ButtonType == EEventType.DynamicButton)
        {
            D_ButData.bIsActive = true;
            D_ButData.EndTime = D_ButData.ShowTimeRate;
            StartCoroutine(ShowTimer());
        }
            
    }

    public void AddShowEndTime()
    {
        if (ButtonType == EEventType.DynamicButton)
        {
            D_ButData.EndTime += D_ButData.ShowTimeRate;
        }
    }

    private void OnDisable()
    {
        Debug.Log("OnDisable called.");
    }

    IEnumerator ShowTimer()
    {
        float time = 0;
        while (time < D_ButData.EndTime)
        {
            time += Time.deltaTime;
            yield return null; // 다음 프레임까지 대기
        }

       D_ButData.bIsActive = false;
       gameObject.SetActive(false);
    }
}
