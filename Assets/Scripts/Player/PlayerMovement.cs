
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] InputReader input;

    [SerializeField] float moveSpeed = 6f;
    [SerializeField] float jumpForce = 8f;
    [SerializeField] float fallMultiplier = 2.5f;
    Rigidbody rb;

    void Awake()
    {
        input.EnableInputMap();
        rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        input.onJumpPressed += Jump;
    }

    void OnDisable()
    {
        input.onJumpPressed -= Jump;
    }

    void FixedUpdate()
    {
        Move();

        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
    }

    void Move()
    {
        var move = new Vector3(input.MoveDirection.x, 0f, input.MoveDirection.y);
        rb.linearVelocity = new Vector3(move.x * moveSpeed,rb.linearVelocity.y,move.z * moveSpeed);
    }

    void Jump()
    {
        rb.linearVelocity= new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
    }


     

}
