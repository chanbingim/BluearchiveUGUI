using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public enum AnimationPlayType
{
    ForwardPlay,
    ReversePlay,
    PingPang,
};

public class CustomUIAnimationBase : MonoBehaviour
{
    public delegate void AnimationEvent();

    #region //variable
    [SerializeField] protected AnimationPlayType WrapMode;
    [SerializeField] protected Vector3 StartVariable;
    [SerializeField] protected Vector3 EndVariable;
    [SerializeField] protected bool bIsLoop = false;
    [SerializeField] protected bool StartPlayAnimation = false;
     
    protected bool bIsPaused = false;
    protected bool bIsPlaying = false;
    protected IEnumerator PlayAni = null;
    protected IEnumerator[] enumerators;

    public float playTime;
    public float playSpeed;
    public GameObject ApplyObejctUI;
    public AnimationEvent EndAnimation;
    public AnimationEvent StartAnimation;
    #endregion
    private void Start()
    {
        Init();
        if (StartPlayAnimation)
            Play();

    }
    public virtual void Play() { }
    public virtual void Init() { }
    public virtual void Pause()
    {
        bIsPaused = true;
    }
    public virtual void Resume()
    {
        bIsPaused = false;
    }
    public virtual void Stop() { }

    protected IEnumerator PLAYPingPongAnimation(IEnumerator[] animation)
    {
        Init();

        // Fade In
        yield return StartCoroutine(animation[0]);
        // 잠시 대기
        yield return new WaitForSeconds(0.5f);
        // Fade Out
        yield return StartCoroutine(animation[1]);

        if (bIsLoop)
            Play();
        if (!bIsLoop)
            bIsPlaying = false;
    }
    public void SetStartVariable(Vector2 pos) { StartVariable = pos; }
    public void SetSEndtVariable(Vector2 pos) { EndVariable = pos; }
    public bool GetAnimationIsPlaying() { return bIsPlaying; }
    public void ChangeAnimationType(AnimationPlayType changeType) { WrapMode = changeType; }
}
