using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonController : MonoBehaviour
{
    [Header("References")]
    public CharacterController controller;
    public Transform cam;

    [Header("Settings")]
    float speed = 5f;
    public float moveSpeed = 5f;
    public float jumpHeight = 2f;
    public float turnSmoothTime = 0.1f;
    public float rollSpeed = 10f;
    public float sidestepSpeed = 8f;
    

    private Vector2 input;
    private float verticalVelocity;
    private float gravity = -10f;
    float turnSmoothVelocity;
    public bool isMoving;
    public bool isRunning;

    //things to add:
    // crouch
    // roll
    // sidestep

    void Update()
    {
        if (controller.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }
        

        Vector3 horizontalMove = new Vector3(input.x, 0, input.y);
        horizontalMove = Quaternion.Euler(0, cam.eulerAngles.y, 0) * horizontalMove;

        Vector3 verticalMove = new Vector3(0, verticalVelocity, 0);

        if (horizontalMove.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(horizontalMove.x, horizontalMove.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }
        controller.Move(horizontalMove * speed * Time.deltaTime + verticalMove * Time.deltaTime);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && controller.isGrounded)
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        // Implement crouch logic here
        // handle animation, adjust character controller height, and reset the height after standing up
        // also maybe reduce speed while crouching
    }

    public void OnRoll(InputAction.CallbackContext context)
    {
        // Implement roll logic here
        speed = rollSpeed;
        //handle animations, perhaps invinciblity, and reset speed after animation
        //if (animationFinished)
        //    speed = moveSpeed;
    }

    public void OnSidestep(InputAction.CallbackContext context)
    {
        speed = sidestepSpeed;
        //handle animations, perhaps invinciblity, and reset speed after animation
        //if (animationFinished)
        //    speed = moveSpeed;
    }
}
