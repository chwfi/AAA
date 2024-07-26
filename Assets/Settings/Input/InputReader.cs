using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "SO/InputReader")]
public class InputReader : ScriptableObject, Controls.IPlayerActions
{
    public Vector2 MoveInput;
    public Vector2 Look;
    public bool Sprint;

    public event Action BasicAttackEvent;
    public event Action DodgeEvent;
    public event Action ParryingEvent;

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
            BasicAttackEvent?.Invoke();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
            DodgeEvent?.Invoke();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        Look = context.ReadValue<Vector2>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();
    }

    public void OnParrying(InputAction.CallbackContext context)
    {
        if (context.performed)
            ParryingEvent?.Invoke();
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (MoveInput != Vector2.zero) 
            Sprint = context.ReadValueAsButton();
    }
}
