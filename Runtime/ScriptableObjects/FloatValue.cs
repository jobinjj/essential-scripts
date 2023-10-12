using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FloatValue", menuName = "ScriptableObjects/FloatValue")]
public class FloatValue : ScriptableObject
{
    [SerializeField]
    private float value;
    public Action<float> OnValueChanged;
    public void SetValue(float value)
    {
        this.value = value;
       // Debug.Log("Set value:" + value);
        OnValueChanged?.Invoke(value);
    }
    public float GetValue()
    {
        return value;
    }
}
