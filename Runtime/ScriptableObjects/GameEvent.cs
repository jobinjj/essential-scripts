using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="GameEvent", menuName ="ScriptableObjects/GameEvent")]
public class GameEvent : ScriptableObject
{
    public Action Event; 
   public void Raise()
    {
        Event?.Invoke();
    }
}
