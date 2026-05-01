using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    private ThirdPersonController movement;
    private PlayerCombat combat;

    void Awake()
    {
        animator = GetComponent<Animator>();
        movement = GetComponentInParent<ThirdPersonController>();
        combat = GetComponentInParent<PlayerCombat>();
    }

    void Update()
    {
        Vector3 localVelocity = movement.transform.InverseTransformDirection(movement.currentVelocity);
        animator.SetFloat("VelocityX", localVelocity.x);
        animator.SetFloat("VelocityZ", localVelocity.z);

        animator.SetFloat("Speed", movement.currentVelocity.magnitude / movement.runSpeed);

        animator.SetBool("IsGrounded", movement.controller.isGrounded);
        animator.SetBool("IsBlocking", combat.isBlocking);
        animator.SetBool("isArmed", combat.isArmed);
    }
}
