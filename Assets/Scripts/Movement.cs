using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public void OnMovementInput(InputAction.CallbackContext ctx)
    {
        Vector2 movementInput = ctx.ReadValue<Vector2>();
    }

    public void OnJumpInput(InputAction.CallbackContext ctx)
    {
        Jump();
    }

    private void Jump()
    {
        throw new System.NotImplementedException();
    }
}
