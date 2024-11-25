using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFadeInOut : CustomUIAnimationBase
{
    private Image _Image;
    private Text _Text;

    private void OnDestroy()
    {
        StopCoroutine(FadeInOut());
    }

    IEnumerator PLAYUIFadeInOutAnimation(float Start, float End)
    {
        float Timer = 0f;
        while (Timer < playTime)
        {
            Timer += Time.unscaledDeltaTime;
            _Image.color = new Color(1,1,1, Mathf.Lerp(Start, End, Timer/ playTime));
            _Text.color = new Color(0.015f, 0.717f, 255/255, Mathf.Lerp(Start, End, Timer/ playTime));
            yield return null;
        }

        if (WrapMode != AnimationPlayType.PingPang && bIsLoop)
            Play();
        else if (WrapMode != AnimationPlayType.PingPang && !bIsLoop)
            bIsPlaying = false;
    }

    private IEnumerator FadeInOut()
    {
         // Fade In
         yield return StartCoroutine(PLAYUIFadeInOutAnimation(0, 1));

         // 잠시 대기
         yield return new WaitForSeconds(1);

         // Fade Out
         yield return StartCoroutine(PLAYUIFadeInOutAnimation(1, 0));

         // 잠시 대기
         yield return new WaitForSeconds(1);

        if (bIsLoop)
            Play();
        else
            bIsPlaying = false;

    }

    public override void Play()
    {
        IEnumerator PlayAni = null;
        _Image = ApplyObejctUI.GetComponent<Image>();
        _Text = ApplyObejctUI.GetComponentInChildren<Text>();

        switch (WrapMode)
        {
            case AnimationPlayType.ForwardPlay:
                PlayAni = PLAYUIFadeInOutAnimation(0, 1);
                break;

            case AnimationPlayType.ReversePlay:
                PlayAni = PLAYUIFadeInOutAnimation(1, 0);
                break;

            case AnimationPlayType.PingPang:
                PlayAni = FadeInOut();
                break;
        }

        if (PlayAni != null)
        {
            bIsPlaying = true;
            StartCoroutine(PlayAni);
        }
    }
}
