using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class UIMoveAnimation : CustomUIAnimationBase
{
    public override void Init()
    {
        enumerators = new IEnumerator[] { MoveAnimation(StartVariable, EndVariable), MoveAnimation(EndVariable, StartVariable) };
    }

    IEnumerator MoveAnimation(Vector3 Start, Vector3 End)
    {
        var transform = ApplyObejctUI.GetComponent<RectTransform>();
        float Timer = 0f;

       transform.anchoredPosition = Start;
        while (Timer < playTime)
        {
            Timer += Time.deltaTime;
            
            transform.anchoredPosition = Vector3.Lerp(Start, End, Timer/ playTime);
            yield return null;
        }

         transform.anchoredPosition = End;

        if (WrapMode != AnimationPlayType.PingPang && bIsLoop)
            Play();
        else if(WrapMode != AnimationPlayType.PingPang && !bIsLoop)
            bIsPlaying = false;
    }

    public override void Play()
    {
        switch (WrapMode)
        {
            case AnimationPlayType.ForwardPlay:
                PlayAni = MoveAnimation(StartVariable, EndVariable);
                break;

            case AnimationPlayType.ReversePlay:
                PlayAni = MoveAnimation(EndVariable, StartVariable);
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
}
