using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIMoveAnimation : CustomUIAnimationBase
{
    public override void Stop()
    {
        StopCoroutine(PLAYUIGroupMoveAnimation());
    }

    IEnumerator MoveAnimation(Vector3 Start, Vector3 End)
    {
        var transform = ApplyObejctUI.GetComponent<RectTransform>();
        float Timer = 0f;

       transform.anchoredPosition = Start;
        while (Timer < playTime)
        {
            Timer += Time.unscaledDeltaTime;
            
            transform.anchoredPosition = Vector3.Lerp(Start, End, Timer/ playTime);
            yield return null;
        }

         transform.anchoredPosition = End;

        if (WrapMode != AnimationPlayType.PingPang && bIsLoop)
            Play();
        else if(WrapMode != AnimationPlayType.PingPang && !bIsLoop)
            bIsPlaying = false;
    }
    private IEnumerator PLAYUIGroupMoveAnimation()
    {
         // Fade In
         yield return StartCoroutine(MoveAnimation(StartVariable, EndVariable));
         // 잠시 대기
         yield return new WaitForSeconds(0.5f);
         // Fade Out
         yield return StartCoroutine(MoveAnimation(EndVariable, StartVariable));

        if (bIsLoop)
            Play();
        else
            bIsPlaying = false;
    }
    public override void Play()
    {
        IEnumerator PlayAni = null;

        switch (WrapMode)
        {
            case AnimationPlayType.ForwardPlay:
                PlayAni = MoveAnimation(StartVariable, EndVariable);
                break;

            case AnimationPlayType.ReversePlay:
                PlayAni = MoveAnimation(EndVariable, StartVariable);
                break;

            case AnimationPlayType.PingPang:
                PlayAni = PLAYUIGroupMoveAnimation();
                break;
        }

        if (PlayAni != null)
        {
            bIsPlaying = true;
            StartCoroutine(PlayAni);
        }
           
    }
}
