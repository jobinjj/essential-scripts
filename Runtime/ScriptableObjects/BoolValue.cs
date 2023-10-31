using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="Bool Value", menuName ="ScriptableObjects/Bool Value")]
public class BoolValue : ScriptableObject
{
    [SerializeField]
    private bool value;
    public Action OnValueChanged;

    public void SetValue(bool val)
    {
        value = val;
        OnValueChanged?.Invoke();
    }
    public bool GetValue()
    {
        return value;
    }

}
