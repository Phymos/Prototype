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
        //animator.SetBool("OnAir", movement.onAir);

        animator.SetBool("IsGrounded", movement.controller.isGrounded);
        animator.SetBool("IsBlocking", combat.isBlocking);
        animator.SetBool("IsArmed", combat.isArmed);

        animator.SetLayerWeight(1, movement.isCrouching ? 1 : 0);
    }


    void OnEnable()
    {
        ThirdPersonController.OnRolling += PlayRoll;
        PlayerCombat.OnLightAttacking += PlayAttack;
        ThirdPersonController.OnJumping += PlayJump;
    }

    void OnDisable()
    {
        ThirdPersonController.OnRolling -= PlayRoll;
        PlayerCombat.OnLightAttacking -= PlayAttack;
        ThirdPersonController.OnJumping -= PlayJump;
    }

    void PlayRoll() => animator.SetTrigger("Roll");

    void PlayJump() => animator.SetTrigger("Jump");

    void PlayAttack(int comboIndex)
    {
        animator.SetInteger("ComboIndex", comboIndex);
        animator.SetTrigger("LightAttack");
    }
}
