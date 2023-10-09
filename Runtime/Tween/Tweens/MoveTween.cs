using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTween : Tween
{
    public Action onComplete;
    public Vector3 target;
    public Transform objectTransform;
    public float time;
    public AnimationCurve xCurve,yCurve,zCurve;
    public bool looped;
    public bool InitializOnStart;
    public bool DestroyOnReached = true;
    public Transform targetTransform;
    public override Tween OnComplete(Action action)
    {
        onComplete = action;
        return this;
    }
    public void Initialize(Transform targetObject, Vector3 targetPosition, float time)
    {
        this.time = time;
        this.objectTransform = targetObject;
        this.target = targetPosition;

        xCurve = new AnimationCurve();
        yCurve = new AnimationCurve();
        zCurve = new AnimationCurve();

        xCurve.AddKey(Time.time, targetObject.position.x); //time, value
        xCurve.AddKey(Time.time + time, target.x);

        yCurve.AddKey(Time.time, targetObject.position.y); //time, value
        yCurve.AddKey(Time.time + time, target.y);

        zCurve.AddKey(Time.time, targetObject.position.z); //time, value
        zCurve.AddKey(Time.time + time, target.z);
    }
    
    public void Initialize(Transform targetObject, Transform target, float time)
    {
        this.time = time;
        this.objectTransform = targetObject;
        this.target = target.position;

        xCurve = new AnimationCurve();
        yCurve = new AnimationCurve();
        zCurve = new AnimationCurve();

        xCurve.AddKey(Time.time, targetObject.position.x); //time, value
        xCurve.AddKey(Time.time + time, this.target.x);

        yCurve.AddKey(Time.time, targetObject.position.y); //time, value
        yCurve.AddKey(Time.time + time, this.target.y);

        zCurve.AddKey(Time.time, targetObject.position.z); //time, value
        zCurve.AddKey(Time.time + time, this.target.z);
    }

    public override void SetLooped()
    {
        looped = true;
        xCurve.postWrapMode = WrapMode.PingPong;
        yCurve.postWrapMode = WrapMode.PingPong;
        zCurve.postWrapMode = WrapMode.PingPong;
    }

    // Start is called before the first frame update
    void Start()
    {
        //xCurve = new AnimationCurve();
        //yCurve = new AnimationCurve();
        //zCurve = new AnimationCurve();

        //xCurve.AddKey(Time.time, current.x); //time, value
        //xCurve.AddKey(Time.time + time, target.x);

        //yCurve.AddKey(Time.time, current.y); //time, value
        //yCurve.AddKey(Time.time + time, target.y);

        //zCurve.AddKey(Time.time, current.z); //time, value
        //zCurve.AddKey(Time.time + time, target.z);
        if (InitializOnStart)
        {
            if(targetTransform != null)
            {
                Initialize(transform, targetTransform, time);

            }
            else
            {
                Initialize(transform, transform.position + target, time);
            }
          
            if (looped == true)
            {
                xCurve.postWrapMode = WrapMode.PingPong;
                yCurve.postWrapMode = WrapMode.PingPong;
                zCurve.postWrapMode = WrapMode.PingPong;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 finalPosition = Vector3.one;
        finalPosition.x = xCurve.Evaluate(Time.time);
        finalPosition.y = yCurve.Evaluate(Time.time);
        finalPosition.z = zCurve.Evaluate(Time.time);

        objectTransform.position = finalPosition;
        if(objectTransform.position == target)
        {
            onComplete?.Invoke();
            this.RemoveAnimation();
            if (DestroyOnReached)
            {
                Destroy(gameObject);
            }
            
        }
    }
}
