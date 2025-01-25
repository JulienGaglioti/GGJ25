using UnityEngine;
using System;
using UnityEngine.Events;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class InputManager : MonoBehaviourSingleton<InputManager>
{

    [Header("Character Input Values")]
    public Vector2 MoveInput;
    public Vector2 LookInput; 
    public Vector2 MousePosition;
    public string CurrentControlScheme { get => _playerInput.currentControlScheme; }

    [Header("Mouse Cursor Settings")]
    public bool CursorLocked = true;
    public bool CursorInputForLook = true;
    private PlayerInput _playerInput;

    public UnityAction AttackAction;
    public UnityAction SwitchAction;


    private void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
    }


#if ENABLE_INPUT_SYSTEM

    public void OnMove(InputValue value)
    {
        MoveInput = value.Get<Vector2>();
    }

    public void OnLook(InputValue value)
    {
        LookInput = value.Get<Vector2>();
    }

    public void OnMousePosition(InputValue value)
    {
        MousePosition = value.Get<Vector2>();
    }

    public void OnAttack(InputValue value)
    {
        AttackAction?.Invoke();
    }

    public void OnSwitch(InputValue value)
    {
        SwitchAction?.Invoke();
    }

    public bool ControlSchemeIsMouse()
    {
        return CurrentControlScheme == "Keyboard&Mouse";
    }

#endif

    private void OnApplicationFocus(bool hasFocus)
    {
        SetCursorState(CursorLocked);
    }

    private void SetCursorState(bool newState)
    {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }
}
