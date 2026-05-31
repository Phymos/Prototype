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
        animator.SetBool("IsLanding", movement.isLanding);

        animator.SetFloat("Speed", movement.currentVelocity.magnitude / movement.runSpeed);
        animator.SetBool("OnAir", movement.onAir);

        animator.SetBool("IsGrounded", movement.controller.isGrounded);
        animator.SetBool("IsBlocking", combat.isBlocking);
        animator.SetBool("IsArmed", combat.isArmed);
    }


    void OnEnable()
    {
        ThirdPersonController.OnRolling += PlayRoll;
        PlayerCombat.OnLightAttacking += PlayAttack;
        ThirdPersonController.OnJumping += PlayJump;
        ThirdPersonController.OnLanding += PlayLanding;
        ThirdPersonController.OnHardLanding += PlayHardLanding;
    }

    void OnDisable()
    {
        ThirdPersonController.OnRolling -= PlayRoll;
        PlayerCombat.OnLightAttacking -= PlayAttack;
        ThirdPersonController.OnJumping -= PlayJump;
        ThirdPersonController.OnLanding -= PlayLanding;
        ThirdPersonController.OnHardLanding -= PlayHardLanding;
    }

    void PlayRoll() => animator.Play("Roll");

    void PlayJump() => animator.Play("Jump");

    void PlayAttack(int comboIndex)
    {
        animator.SetInteger("ComboIndex", comboIndex);
        switch(comboIndex)
        {
            case 1: animator.CrossFadeInFixedTime("Attack 1", 0.1f, 0); break;
            case 2: animator.CrossFadeInFixedTime("Attack 2", 0.1f, 0); break;
            case 3: animator.CrossFadeInFixedTime("Attack 3", 0.1f, 0); break;
        }
    }

    void PlayLanding() => animator.SetTrigger("IsLanding");
    void PlayHardLanding() => animator.Play("HardLanding");

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

    public void EnableSword() 
    {
        if (combat != null) combat.EnableSword();
    }

    public void DisableSword() 
    {
        if (combat != null) combat.DisableSword();
    }

    public void AllowNextAttack()
    {
        combat.AllowNextAttack();
    }

    public void ResetCombo()
    {
        combat.ResetCombo();
    }
}
