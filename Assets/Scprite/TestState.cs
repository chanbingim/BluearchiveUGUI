using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestState : MonoBehaviour
{
    [SerializeField] bool bIsVisible = false;
    public Text fpsText;
    public Text audioCountText;
    public Text audioLevelText;

    float fps;
    int AudioCount = 3;
    float AudioLevel;

    public void OnClickVisible()
    {
        if (bIsVisible)
            bIsVisible = false;
        else
            bIsVisible = true;

       this.gameObject.SetActive(bIsVisible);
    }

    public void ChangeMasterVolum(float value)
    {
        AudioLevel = value;
    }

    private void Update()
    {
        fps = 1.0f / Time.deltaTime;

        fpsText.text = "FPS : " + fps.ToString("N2");
        audioCountText.text = "Audio Count : " + AudioCount.ToString();
        audioLevelText.text = "Audio Level : " + AudioLevel.ToString("N2");
    }



    


}
