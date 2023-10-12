

using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;

public class MobileUIDrag : MonoBehaviour, IDragHandler, IEndDragHandler{

	//private bool isPressing = false;
	public static Action<PointerEventData> OnDragging;
	public static Action OnDragFinished;
    public static Action<float> OnPinch;
    public FloatValue DragDeltaX, DragDeltaY;
    private float lastDragTime;
    public bool UpdateDuringDrag;
    

    public void OnDrag(PointerEventData data){
            OnDragging?.Invoke(data);
        DragDeltaX.SetValue(data.delta.x);
        DragDeltaY.SetValue(data.delta.y);
        lastDragTime = Time.time;
    }
    

	public void OnEndDrag(PointerEventData data)
    {
        OnDragFinished?.Invoke();
        ResetInput();
    }

    private void ResetInput()
    {
        DragDeltaX.SetValue(0);
        DragDeltaY.SetValue(0);
    }

    private void Update()
    {
        if(UpdateDuringDrag && Time.time > lastDragTime + 0.1f)
        {
            ResetInput();
        }
    }

}
