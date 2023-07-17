using System;

namespace Player
{
    

using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private TouchControls _touchControls;

    public void Awake()
    {
        _touchControls = new TouchControls();
    }

    public void OnEnable()
    {
        _touchControls.Enable();
    }

    public void OnDisable()
    {
        _touchControls.Disable();
    }

    public void Start()
    {
        _touchControls.Touch.TouchPress.started += ctx => StartTouch(ctx);
        _touchControls.Touch.TouchPress.canceled += ctx => EndTouch(ctx);
    }

    private void StartTouch(InputAction.CallbackContext ctx)
    {
        Debug.Log("Touch Started at pos " + _touchControls.Touch.TouchPosion.ReadValue<Vector2>());
    }
    
    private void EndTouch(InputAction.CallbackContext ctx)
    {
        Debug.Log("Touch canceled at pos " + _touchControls.Touch.TouchPosion.ReadValue<Vector2>());
    }
}
}
