using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenManager : MonoBehaviour
{
    public List<Tween> runningTweens = new List<Tween>();


    private static TweenManager instance;
    public static TweenManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject().AddComponent<TweenManager>();
                instance.name = "SAController";
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }
 


    public Tween Scale(Transform targetObject, float targetScale, float time)
    {
        //exit if animation already started usefull if called from update call
        foreach (Tween scriptAnimation in runningTweens)
        {
            if (scriptAnimation.transform == targetObject)
            {
                return null;
            }
        }

        ScaleTween scaleAnimation = NewObject("ScaleLoop").AddComponent<ScaleTween>();
        scaleAnimation.Initialize(targetObject, targetScale, time);
        runningTweens.Add(scaleAnimation);
        return scaleAnimation;
    }

    public FloatLerpTween FloatLerp(float floatVal, float targetVal, float time)
    {
        //exit if animation already started usefull if called from update call
        foreach (Tween scriptAnimation in runningTweens)
        {
            if (scriptAnimation.transform == transform)
            {
                return null;
            }
        }

        FloatLerpTween floatLerpTween = NewObject("FloatLerp").AddComponent<FloatLerpTween>();
        floatLerpTween.current = floatVal;
        floatLerpTween.target = targetVal;
        floatLerpTween.time = time;
        runningTweens.Add(floatLerpTween);
        return floatLerpTween;
    }

    public IntLerpTween IntLerp(int current, float target, float time)
    {
        IntLerpTween intLerpTween = NewObject("IntLerp").AddComponent<IntLerpTween>();
       
        return intLerpTween;
    }

    public MoveTween Move(Transform transform, Vector3 target, float time)
    {
        MoveTween moveTween = NewObject("MoveTween").AddComponent<MoveTween>();
        moveTween.Initialize(transform, target, time);
        runningTweens.Add(moveTween);
        return moveTween;
    }

    public void RemoveAnimation(Tween scriptAnimation)
    {
        if (runningTweens.Contains(scriptAnimation))
        {
            runningTweens.Remove(scriptAnimation);
            Destroy(scriptAnimation.gameObject);
        }
       
    }

    private GameObject NewObject(string name)
    {
        GameObject obj = new GameObject();
        obj.transform.parent = transform;
        obj.name = name;
        return obj;
    }
}
public static class AnimationExtension
{
    public static Tween Scale(this Transform tr, float targetScale, float time)
    {
        return TweenManager.Instance.Scale(tr, targetScale, time);
    }

    public static FloatLerpTween Lerp(this float floatVal, float targetVal, float time)
    {

        return TweenManager.Instance.FloatLerp(floatVal, targetVal, time);

    }

    public static IntLerpTween Lerp(this int intVal, float targetVal, float time)
    {

        return TweenManager.Instance.IntLerp(intVal, targetVal, time);

    }

    public static MoveTween Move(this Transform transform, Vector3 target, float time)
    {
        return TweenManager.Instance.Move(transform, target, time);
    }

    public static void RemoveAnimation(this Tween scriptAnimation)
    {
        TweenManager.Instance.RemoveAnimation(scriptAnimation);

    }

    public static void SetLooped(this Tween tween)
    {
        tween.SetLooped();
    }





    //public static void RemoveAnimation(this Transform tr, float restoreScale)
    //{
    //    SAController.Instance.RemoveScaleLoopAnimation(tr, restoreScale);
    //}
}

