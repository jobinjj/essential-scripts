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
    public void Increment(int incrementValue)
    {
        value += incrementValue;
        OnValueChanged?.Invoke(value);
    }
}
public static class IntValueExtensions
{
    public static void Increment(this IntValue intValue, int incrementValue = 1)
    {
        int currentVal = intValue.GetValue();
        currentVal += incrementValue;
        intValue.SetValue(currentVal);
    }
    public static void Decrement(this IntValue intValue, int decrementValue = 1)
    {
        int currentVal = intValue.GetValue();
        currentVal -= decrementValue;
        intValue.SetValue(currentVal);
    }
}