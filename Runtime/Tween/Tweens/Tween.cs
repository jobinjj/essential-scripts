using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tween : MonoBehaviour
{

    public Action CompleteAction;
    public Action<float> FloatValueChangeAction;

    public abstract Tween OnComplete(Action action);
    public abstract void SetLooped();
  

}


