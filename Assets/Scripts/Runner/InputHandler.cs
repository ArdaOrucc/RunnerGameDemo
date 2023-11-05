using System;
using UnityEngine;

public class InputHandler
{
    public Action OnPointerDownAction;
    public Action OnPointerUpAction;
    public Action OnPointerAction;
    
    public Vector2 PointerDownPosition;
    public Vector2 PointerPosition;
    public Vector2 PointerUpPosition;
    
    public Vector2 DeltaPosition => new Vector2(
        (PointerPosition.x - PointerDownPosition.x) / Screen.width,
        (PointerPosition.y - PointerDownPosition.y) / Screen.height
        );

    public void PointerUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnPointerDown();
        }

        if (Input.GetMouseButton(0))
        {
            OnPointer();
        }

        if (Input.GetMouseButtonUp(0))
        {
            OnPointerUp();
        }
    }

    private void OnPointerDown()
    {
        PointerDownPosition = Input.mousePosition;
        OnPointerDownAction?.Invoke();
    }

    private void OnPointerUp()
    {
        PointerUpPosition = Input.mousePosition;
        OnPointerUpAction?.Invoke();
    }

    private void OnPointer()
    {
        PointerPosition = Input.mousePosition;
        OnPointerAction?.Invoke();
    }
}
