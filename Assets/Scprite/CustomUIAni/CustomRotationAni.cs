using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CustomRotationAni : CustomUIAnimationBase
{
    [SerializeField]
    private Vector3 StartRot;

    [SerializeField]
    private Vector3 EndRot;

    private void OnDestroy()
    {
        StopCoroutine(PLAYUIRotationAnimation());
    }

    private IEnumerator RotationAnimation(Vector3 start, Vector3 end)
    {
        var transform = ApplyObejctUI.GetComponent<RectTransform>();
        float Timer = 0.0f;

        while (Timer < playTime)
        {
            Timer += Time.unscaledDeltaTime;

            Quaternion newRotation = Quaternion.Euler(Vector3.Lerp(start, end, Timer / playTime));
            transform.rotation = newRotation;

            yield return null;
        }
        transform.localRotation = Quaternion.Euler(end);

        if (WrapMode != AnimationPlayType.PingPang && bIsLoop)
            Play();
        else if (WrapMode != AnimationPlayType.PingPang && !bIsLoop)
            bIsPlaying = false;

    }

    private IEnumerator PLAYUIRotationAnimation()
    {
        yield return StartCoroutine(RotationAnimation(StartRot, EndRot));
        yield return new WaitForSeconds(1);
        yield return StartCoroutine(RotationAnimation(EndRot, StartRot));

        if (bIsLoop)
        {
            Play();
        }
        else
            bIsPlaying = false;
    }

    public override void Play()
    {
        IEnumerator PlayAni = null;

        switch (WrapMode)
        {
            case AnimationPlayType.ForwardPlay:
                PlayAni = RotationAnimation(StartVariable, EndVariable);
                break;

            case AnimationPlayType.ReversePlay:
                PlayAni = RotationAnimation(EndVariable, StartVariable);
                break;

            case AnimationPlayType.PingPang:
                PlayAni = PLAYUIRotationAnimation();
                break;
        }

        if (PlayAni != null)
        {
            bIsPlaying = true;
            StartCoroutine(PlayAni);
        }
            

    }
}
