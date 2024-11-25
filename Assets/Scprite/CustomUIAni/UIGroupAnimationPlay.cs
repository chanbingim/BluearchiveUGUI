using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public enum EGroupAnimationType
{
    sequence,
    PingPongsequence,
    Concurrent
}

public class UIGroupAnimationPlay : MonoBehaviour
{
    [SerializeField] private List<GameObject> AnimationGroup;
    [SerializeField] private AnimationPlayType ChangeType;
    [SerializeField] private EGroupAnimationType GroupPlayType;
    [SerializeField] private bool bIsLoop;
    [SerializeField] private bool StartFramePlayAnimation = false;
    private void Start()
    {
        if (StartFramePlayAnimation)
            PlayGroupAnimation();
    }

    public void PlayGroupAnimation()
    {
        if (GroupPlayType == EGroupAnimationType.Concurrent)
        {
            PlayConcurrentAnimations();
        }
        else
        {
            StartCoroutine(SequentialAnimationCoroutine());
        }
    }

    private void PlayConcurrentAnimations()
    {
        foreach (var obj in AnimationGroup)
        {
            var animation = obj.GetComponent<CustomUIAnimationBase>();
            if (animation)
            {
                animation.ChangeAnimationType(ChangeType);
                animation.Play();
            }
        }
    }

    private IEnumerator SequentialAnimationCoroutine()
    {
        if(GroupPlayType == EGroupAnimationType.sequence)
        {
            foreach (var obj in AnimationGroup)
            {
                var animation = obj.GetComponent<CustomUIAnimationBase>();
                if (animation)
                {
                    StartCoroutine(sequenceElement(() => animation.Play()));

                    while (animation.GetAnimationIsPlaying())
                        yield return null;
                }
            }
        }
        else if (GroupPlayType == EGroupAnimationType.PingPongsequence)
        {
            int i = 0;
            int direction = 1;
            while (bIsLoop)
            {
                var animation = AnimationGroup[i].GetComponent<CustomUIAnimationBase>();
                if (animation)
                {
                    StartCoroutine(sequenceElement(() => animation.Play()));

                    while (animation.GetAnimationIsPlaying())
                        yield return null;
                }

                // 방향에 따라 인덱스 이동
                i += direction;

                // 끝에 도달하면 방향 반전
                if (i >= AnimationGroup.Count)
                {
                    direction = -1; // Reverse
                    i = AnimationGroup.Count - 1; // Adjust index
                }
                else if (i < 0)
                {
                    direction = 1; // Forward
                    i = 0; // Adjust index
                }
            }
        }


    }

    private IEnumerator sequenceElement(System.Action action)
    {
        action.Invoke();
        yield return null;
    }
}
