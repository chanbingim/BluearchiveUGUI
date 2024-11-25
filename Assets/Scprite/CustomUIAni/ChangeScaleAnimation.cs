using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChangeScaleAnimation : CustomUIAnimationBase
{
    public float StartSize = 0;
    public float EndSize = 0;

    public override void Play()
    {
        StartCoroutine(playAnimation());
    }

    IEnumerator playAnimation()
    {
        float time = 0;
        Vector2 V_StartSize = new Vector2(StartSize, StartSize);
        Vector2 V_EndSize = new Vector2(EndSize, EndSize);

        ApplyObejctUI.transform.localScale = V_StartSize;

        while (time < playTime)
        {
            time += Time.deltaTime;
            ApplyObejctUI.transform.localScale = Vector2.Lerp(V_StartSize, V_EndSize, time/playTime);
            yield return null;
        }

        ApplyObejctUI.transform.localScale = Vector2.zero;
        ApplyObejctUI.SetActive(false);
    }

}
