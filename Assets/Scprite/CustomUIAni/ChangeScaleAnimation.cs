using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[Serializable]
public enum EChangeScaleType
{
    Vertical,
    Horizontal,
    Full
}

public class ChangeScaleAnimation : CustomUIAnimationBase
{
    [SerializeField] private EChangeScaleType changeScaleType;
    public float StartSize = 0;
    public float EndSize = 0;

    public override void Init()
    {
        enumerators = new IEnumerator[] { playAnimation(StartSize, EndSize), playAnimation(EndSize, StartSize) };
    }

    public override void Play()
    {
        switch (WrapMode)
        {
            case AnimationPlayType.ForwardPlay:
                PlayAni = playAnimation(StartSize, EndSize);
                break;

            case AnimationPlayType.ReversePlay:
                PlayAni = playAnimation(EndSize, StartSize);
                break;

            case AnimationPlayType.PingPang:
                PlayAni = PLAYPingPongAnimation(enumerators);
                break;
        }

        if (PlayAni != null)
        {
            bIsPlaying = true;
            StartCoroutine(PlayAni);
        }
    }

    IEnumerator playAnimation(float startSize, float endSize)
    {
        float time = 0;

        Vector3 V_StartSize = Vector3.zero;
        Vector3 V_EndSize = Vector3.zero;

        switch (changeScaleType)
        {
            case EChangeScaleType.Vertical:
                V_StartSize = new Vector2(1, startSize);
                V_EndSize = new Vector2(1, endSize);
                break;
            case EChangeScaleType.Horizontal:
                V_StartSize = new Vector2(startSize, 1);
                V_EndSize = new Vector2(endSize, 1);
                break;
            case EChangeScaleType.Full:
                V_StartSize = new Vector2(startSize, startSize);
                V_EndSize = new Vector2(endSize, endSize);
                break;
        }
        ApplyObejctUI.transform.localScale = V_StartSize;

        while (time < playTime)
        {
            time += Time.deltaTime;
            ApplyObejctUI.transform.localScale = Vector2.Lerp(V_StartSize, V_EndSize, time / playTime);
            yield return null;
        }

        ApplyObejctUI.transform.localScale = V_EndSize;
        ApplyObejctUI.SetActive(false);

        if (WrapMode != AnimationPlayType.PingPang && bIsLoop)
            Play();

        if (WrapMode != AnimationPlayType.PingPang && !bIsLoop)
            bIsPlaying = false;

        
    }

}
