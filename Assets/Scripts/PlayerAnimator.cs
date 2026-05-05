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
        animator.SetFloat("VelocityY", localVelocity.y);

        animator.SetBool("IsHardLanding", movement.isHardLanding);

        animator.SetFloat("Speed", movement.currentVelocity.magnitude / movement.runSpeed);
        animator.SetBool("OnAir", movement.onAir);

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

    void PlayRoll() => animator.Play("Roll");

    void PlayJump() => animator.Play("Jump");

    void PlayAttack(int comboIndex)
    {
        animator.SetInteger("ComboIndex", comboIndex);
        animator.SetTrigger("LightAttack");
    }

    public void IsLandingEnable()
    {
        movement.isLanding = true;
    }

    public void IsLandingDisable()
    {
        movement.isLanding = false;
    }

    public void IsHardLandingEnable()
    {
        movement.isHardLanding = true;
    }

    public void IsHardLandingDisable()
    {
        movement.isHardLanding = false;
    }
}
