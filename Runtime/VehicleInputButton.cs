using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VehicleInputButton : MonoBehaviour, IPointerDownHandler,IPointerUpHandler
{
    public FloatValue Input;

    [HideInInspector]
    public bool selected;
    public float gradualUpTime = 1f;
    public float gradualDownTime = 0.2f;
    private FloatLerpTween floatLerp;
    public bool gradual = true;
    public Action OnClick;
    public Action PointerDown;
    public Action PointerUp;
    public bool invert;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        selected = true;
        PointerDown?.Invoke();
        if (gradual)    
        {
            floatLerp = Input.GetValue().Lerp(1, gradualUpTime).OnFloatValueChange((val) =>
            {
                Input.SetValue(invert ? -val : val);
            });
        }else
        {
            Input.SetValue(invert ? -1 : 1);
        }
        OnClick?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        PointerUp?.Invoke();
        if (gradual)
        {
            if (floatLerp != null)
            {
                floatLerp.RemoveAnimation();
            }
            Input.GetValue().Lerp(0, gradualDownTime).OnFloatValueChange((val) =>
            {
                Input.SetValue(val);
            });
        }
        else
        {
            Input.SetValue(0);
            selected = false;
        }
       
    }

    private void OnDisable()
    {
        if(floatLerp != null)
        {
            floatLerp.RemoveAnimation();
        }
        Input.SetValue(0);
    }

}
