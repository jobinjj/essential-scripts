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

    public static void ResetCamera(this Camera camera)
    {
        camera.transform.localPosition = Vector3.zero;
        camera.transform.localRotation = Quaternion.identity;
    }

    public static void ExecuteCoroutine(this MonoBehaviour behaviour, IEnumerator coroutine, Action OnComplete)
    {
        behaviour.StartCoroutine(ExecuteCoroutine(coroutine, OnComplete));
    }
    static IEnumerator ExecuteCoroutine(IEnumerator coroutine, Action OnComplete)
    {
        yield return coroutine;
        OnComplete?.Invoke();
    }

    public static void DestroyIfNotNull<T>(this T objectToDestroy) where T : Component
    {
        if (objectToDestroy != null)
        {
            UnityEngine.Object.Destroy(objectToDestroy.gameObject);
        }
    }
    public static void DestroyIfNotNull(this GameObject objectToDestroy)
    {
        if (objectToDestroy != null)
        {
            UnityEngine.Object.Destroy(objectToDestroy);
        }
    }

    public static void DestroyAllItems<T>(this List<T> list, Action<T> IterateItem) where T : Component
    {
        List<T> tempList = new List<T>();
        foreach (T item in list)
        {
            IterateItem?.Invoke(item);
            tempList.Add(item);
        }
        foreach (T item in tempList)
        {
            list.Remove(item);
            UnityEngine.Object.Destroy(item.gameObject);
        }
    }

    public static void DestroyAllItems<T>(this List<T> list) where T : Component
    {
        List<T> tempList = new List<T>();
        foreach (T item in list)
        {
            tempList.Add(item);
        }
        foreach (T item in tempList)
        {
            list.Remove(item);
            UnityEngine.Object.Destroy(item.gameObject);
        }
    }

    public static T Load<T>(this T so, string name) where T : ScriptableObject
    {
        if (so == null)
        {
            return Resources.Load<T>(name);
        }
        else
        {
            return so;
        }
        //so = Resources.Load<T>(name);
    }
}
