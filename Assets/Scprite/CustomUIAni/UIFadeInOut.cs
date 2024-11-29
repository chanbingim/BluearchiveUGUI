using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIFadeInOut : CustomUIAnimationBase
{
    private Image _Image;
    private Text _Text;
   
    public override void Init()
    {
        enumerators = new IEnumerator[] { PLAYUIFadeInOutAnimation(1, 0), PLAYUIFadeInOutAnimation(0, 1) };
     }

    IEnumerator PLAYUIFadeInOutAnimation(float Start, float End)
    {
        float Timer = 0f;
        while (Timer < playTime)
        {
            Timer += Time.deltaTime;
            _Image.color = new Color(1,1,1, Mathf.Lerp(Start, End, Timer/ playTime));
            _Text.color = new Color(0.015f, 0.717f, 255/255, Mathf.Lerp(Start, End, Timer/ playTime));
            yield return null;
        }

        if (WrapMode != AnimationPlayType.PingPang && bIsLoop)
            Play();

        if (WrapMode != AnimationPlayType.PingPang && !bIsLoop)
            bIsPlaying = false;
    }

    public override void Play()
    {
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
