using System;
using System.Collections;
using System.Collections.Generic;
using Spine;
using Spine.Unity;
using UnityEngine;
using UnityEngine.Events;
using Event = Spine.Event;

public class SendMessageSkeletonAnimation : MonoBehaviour
{
    public UnityEvent<string> OnEvent;
    
    [SerializeField] private SkeletonAnimation _skeletonAnimation;

    private void Reset()
    {
        _skeletonAnimation = GetComponent<SkeletonAnimation>();
    }

    private void OnEnable()
    {
        _skeletonAnimation.AnimationState.Event += AnimationStateOnEvent; 
    }

    private void OnDisable()
    {
        _skeletonAnimation.AnimationState.Event -= AnimationStateOnEvent;
    }

    private void AnimationStateOnEvent(TrackEntry trackentry, Event e)
    {
        gameObject.SendMessage(e.Data.Name);
    }
}
