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

    [SerializeField] protected AnimationPlayType WrapMode;
    [SerializeField] protected Vector3 StartVariable;
    [SerializeField] protected Vector3 EndVariable;
    [SerializeField] protected bool bIsLoop = false;
    [SerializeField] protected bool StartPlayAnimation = false;
     
    protected bool bIsPaused = false;
    protected bool bIsPlaying = false;

    public float playTime;
    public float playSpeed;
    public GameObject ApplyObejctUI;
    public AnimationEvent EndAnimation;
    public AnimationEvent StartAnimation;

    private void Start()
    {
        if (StartPlayAnimation)
            Play();

    }
    public virtual void Play() { }
    public virtual void Pause()
    {
        bIsPaused = true;
    }
    public virtual void Resume()
    {
        bIsPaused = false;
    }
    public virtual void Stop() { }

    public void SetStartVariable(Vector2 pos) { StartVariable = pos; }
    public void SetSEndtVariable(Vector2 pos) { EndVariable = pos; }
    public bool GetAnimationIsPlaying() { return bIsPlaying; }

    public void ChangeAnimationType(AnimationPlayType changeType) { WrapMode = changeType; }
}
