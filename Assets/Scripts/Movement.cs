using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float maxSpeed;

    private Vector3 moveForce=Vector3.zero;

    private Rigidbody rb;
    private Vector3 movementInput;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        MovePlayer();
        CounterMovement();
    }

    private void MovePlayer()
    {
        moveForce = movementInput*moveSpeed;
        rb.AddForce(moveForce);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed); 
    }

    private void CounterMovement()
    {
        bool noInput = Mathf.Approximately(Vector3.SqrMagnitude(movementInput), 0);
        bool oppositeInput = Vector3.Dot(rb.velocity, movementInput) <= 0;
        if (noInput)
        {
            Vector3 counterForce = rb.velocity * (-1f * 0.99f);
            rb.AddForce(counterForce);
        }
        else if (oppositeInput)
        {
            rb.AddForce(moveForce*3f);
        }
    }
    
    public void OnMovementInput(InputAction.CallbackContext ctx)
    {
        Vector2 movementInput2d = ctx.ReadValue<Vector2>();
        movementInput2d=movementInput2d.normalized;
        movementInput = new Vector3(movementInput2d.x, 0, movementInput2d.y);
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
