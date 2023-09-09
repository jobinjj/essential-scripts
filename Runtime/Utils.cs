using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{

    public static void WaitForSeconds(this MonoBehaviour behaviour, float time, Action OnComplete)
    {
        behaviour.StartCoroutine(WaitForSeconds(time, OnComplete));
    }
    public static void WaitUntil(this MonoBehaviour behaviour, ref bool condition, Action OnComplete)
    {
        behaviour.StartCoroutine(WaitUntil(condition, OnComplete));
    }

    static IEnumerator WaitForSeconds(float time, Action OnComplete)
    {
        yield return new WaitForSeconds(time);
        OnComplete?.Invoke();
    }
    static IEnumerator WaitUntil(bool condition, Action OnComplete)
    {
        yield return new WaitUntil(() => condition);
        OnComplete?.Invoke();
    }
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = UnityEngine.Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public static string Color(this string text, string color)
    {
        return "<color=" + color + ">" + text + "</color>";
    }
}
