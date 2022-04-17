using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    private Rigidbody rb;
    private Vector2 movementInput;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        rb.velocity = moveSpeed * (new Vector3(movementInput.x, rb.velocity.y, movementInput.y));
    }
    
    public void OnMovementInput(InputAction.CallbackContext ctx)
    {
        movementInput = ctx.ReadValue<Vector2>();
        Debug.Log(movementInput);
    }

    public void OnJumpInput(InputAction.CallbackContext ctx)
    {
        Jump();
    }

    private void Jump()
    {
        rb.AddForce(jumpForce*Vector3.up);
    }
}
