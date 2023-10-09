using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FloatLerpTween : Tween
{
    public float current;
    public float target;
    public AnimationCurve curve;
    public Action completeAction;
    public float time;

    public override Tween OnComplete(Action action)
    {
        this.completeAction = action;
        return this;
    }

    public void Start()
    {
        curve = new AnimationCurve();
        curve.AddKey(Time.time, current); //time, value
        curve.AddKey(Time.time + time, target);
      //  curve.postWrapMode = WrapMode.PingPong;
    }

      public FloatLerpTween OnFloatValueChange(Action<float> action) {
        FloatValueChangeAction = action;
        
        return this;
    }

    public void Update()
    {
        current = curve.Evaluate(Time.time);

        FloatValueChangeAction?.Invoke(current);
        if (current == target)
        {
            completeAction?.Invoke();
            this.RemoveAnimation();
            Destroy(gameObject);
        }
    }

    public override void SetLooped()
    {
    }
}
