using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonController : MonoBehaviour
{
    [Header("References")]
    public CharacterController controller;
    public Transform cam;

    [Header("Settings")]
    public float currentSpeed = 3f;
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public float jumpHeight = 2f;
    public float turnSmoothTime = 0.2f;
    public float sidestepSpeed = 8f;
    [SerializeField] AnimationCurve rollCurve;
    public float rollTurnSpeed = 180f;

    public static event Action OnJumping;
    public bool onAir;
    private bool isJumping;
    public static event Action OnRolling;
    bool isRolling;
    float rollTimer;
    private Vector2 input;
    private float verticalVelocity;
    private float gravity = -10f;
    float turnSmoothVelocity;
    public bool isMoving;
    public bool isRunning;
    public bool isCrouching;


    public Vector3 currentVelocity => controller.velocity;

    //things to add:
    // crouch
    // roll
    // sidestep

    void Start()
    {
        Keyframe rollLastFrame = rollCurve[rollCurve.length - 1];
        rollTimer = rollLastFrame.time;
    }

    void Update()
    {
        if (isRolling)
            return;

        if (controller.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        if (!controller.isGrounded && !isJumping)
        {
            onAir = true;
        }
        
        if (controller.isGrounded)
        {
            onAir = false;
            isJumping = false;
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
        controller.Move(horizontalMove * currentSpeed * Time.deltaTime + verticalMove * Time.deltaTime);

        if (!controller.isGrounded && !isJumping)
        {
            onAir = true;
        }
        
        if (controller.isGrounded)
        {
            onAir = false;
            isJumping = false;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && controller.isGrounded && !isCrouching && !isRolling)
        {
            isJumping = true;
            OnJumping?.Invoke();
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.performed && controller.isGrounded)
        {
            isCrouching = true;
            controller.height = 1.5f;
            controller.center = new Vector3(0, -0.10f, 0);
        }
        else if (context.canceled)
        {
            isCrouching = false;
            controller.height = 1.75f;
            controller.center = new Vector3(0, 0.03f, 0);
        }
        // handle animation, adjust character controller height, and reset the height after standing up
        // also maybe reduce speed while crouching
    }

    public void OnRoll(InputAction.CallbackContext context)
    {
        if (context.performed && controller.isGrounded && isRolling == false)
        {
            StartCoroutine(RollCoroutine());
        }
    }

    public void OnSidestep(InputAction.CallbackContext context)
    {
        currentSpeed = sidestepSpeed;
        //handle animations, perhaps invinciblity, and reset speed after animation
        //if (animationFinished)
        //    speed = moveSpeed;
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            currentSpeed = runSpeed;
            isRunning = true;
        }
        else if (context.canceled)
        {
            currentSpeed = walkSpeed;
            isRunning = false;
        }
    }

    IEnumerator RollCoroutine()
    {   
        OnRolling?.Invoke();
        isRolling = true;
        float timer = 0f;
        
        while (timer < rollTimer)
        {
            if (!controller.isGrounded)
            {
                verticalVelocity += gravity * Time.deltaTime;
            }
            else
            {
                verticalVelocity = -2f;
            }

            Vector3 moveInput = new Vector3(input.x, 0, input.y);
            if (moveInput.magnitude > 0.1f)
            {
                Vector3 targetDirection = Quaternion.Euler(0, cam.eulerAngles.y, 0) * moveInput;
                Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rollTurnSpeed * Time.deltaTime);
            }

            float rollSpeed = rollCurve.Evaluate(timer);
        
            Vector3 moveDelta = (transform.forward * rollSpeed) + (Vector3.up * verticalVelocity);
            controller.Move(moveDelta * Time.deltaTime);

            timer += Time.deltaTime;
            yield return null;
        }


        isRolling = false;
    }
}
