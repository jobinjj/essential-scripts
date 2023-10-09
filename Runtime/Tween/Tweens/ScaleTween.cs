using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ScaleTween : Tween
{
    public Transform targetObject;
    public float targetScale;
    public AnimationCurve curve;
    public float time;
    private Action completeAction;
    public bool looped;
    public bool InitializOnStart;

    public override Tween OnComplete(Action action)
    {
        completeAction = action;
        return this;
    }

    public override void SetLooped()
    {
        looped = true;
        curve.postWrapMode = WrapMode.PingPong;
    }

    public void Start()
    {
        if (InitializOnStart)
        {
            Initialize(transform, targetScale, time);
            if(looped == true)
            {
                curve.postWrapMode = WrapMode.PingPong;
            }
        }
    }

    public void Initialize(Transform targetObject, float targetScale, float time)
    {
        this.time = time;
        this.targetObject = targetObject;
        this.targetScale = targetScale;
        curve = new AnimationCurve();
        curve.AddKey(Time.time, targetObject.localScale.x); //time, value
        curve.AddKey(Time.time + time, targetScale);
    }


    public void Update()
    {
        if (targetObject != null)
        {
            targetObject.localScale = new Vector3(curve.Evaluate(Time.time), curve.Evaluate(Time.time), curve.Evaluate(Time.time));
            if(targetObject.localScale == Vector3.one * targetScale && looped == false)
            {
                this.RemoveAnimation();
                Destroy(gameObject);
            }
        }
    }
}
