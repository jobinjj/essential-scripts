using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="IntValue", menuName ="ScriptableObjects/IntValue")]
public class IntValue : ScriptableObject
{
    [SerializeField]
    private int value;
    public Action<int> OnValueChanged;
    public void SetValue(int value)
    {
        this.value = value;
        OnValueChanged?.Invoke(value);
    }
    public int GetValue()
    {
        return value;
    }
}
