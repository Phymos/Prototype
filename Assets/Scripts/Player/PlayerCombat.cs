using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    [Header("References")]
    public LayerMask enemyLayers;

    [Header("Attack Settings")]
    public float lightAttackDamage = 20f;
    public float heavyAttackDamage = 40f;
    public float attackBufferTime = 0.5f;
    private int comboIndex = 0;

    private bool canAttack = true;
    private bool comboQueued = false;

    public Collider swordCollider;
    public List<GameObject> alreadyHit = new List<GameObject>();
    
    public bool isBlocking = false;
    public bool isArmed = false;
    public bool isAttacking = false;

    private CharacterController characterController;
    private ThirdPersonController thirdPersonController;
    private LockOnSystem lockOnSystem;
    public ElementSO currentElement;
    
    public static event Action<int> OnLightAttacking;
    private MagicAttack magicAttack;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        thirdPersonController = GetComponent<ThirdPersonController>();
        lockOnSystem = GetComponent<LockOnSystem>();
        magicAttack = GetComponent<MagicAttack>();
    }

    void Update()
    {
        if (comboQueued && canAttack)
        {
            comboQueued = false;
            PerformLightAttack();
        }
    }

    public void EnableSword() 
    { 
        alreadyHit.Clear();
        swordCollider.enabled = true; 
    }

    public void DisableSword() 
    { 
        swordCollider.enabled = false; 
    }

    public void OnLightAttack(InputAction.CallbackContext context)
    {
        if (!context.performed || !isArmed || thirdPersonController.onAir || thirdPersonController.isHardLanding || thirdPersonController.isRolling) return;

        comboQueued = true;
    }

    void OnHeavyAttack(InputAction.CallbackContext context)
    {
        if (!isArmed) return;
    }

    void OnBlock(InputAction.CallbackContext context)
    {
        if (!isArmed) return;
        
        if (context.performed)
            isBlocking = true;
        else if (context.canceled)
            isBlocking = false;
    }

    void OnParry(InputAction.CallbackContext context)
    {
        if (!isArmed) return;
        
        // Implement parry logic here
    }

    public void OnDrawSword()
    {
        if (isArmed)
            isArmed = false;
        else
            isArmed = true;
    }

    public void OnMagic1(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y - 0.9f, transform.position.z);
            GameObject vfx = Instantiate(currentElement.attackVFX, spawnPos, transform.rotation);
            Destroy(vfx, 2f);

            magicAttack.CastCone(10f, 60f, currentElement.baseDamage, 5f, enemyLayers);
        }
    }

    void PerformLightAttack()
    {
        canAttack = false;
        comboIndex++;

        if (comboIndex > 3) comboIndex = 1;

        OnLightAttacking?.Invoke(comboIndex);
        isAttacking = true;
        StartCoroutine(AttackLunge(0.4f, 4f));      
    }

    public IEnumerator HitStop(float duration)
{
    Time.timeScale = 0f;
    yield return new WaitForSecondsRealtime(duration);
    Time.timeScale = 1f;
}

    IEnumerator AttackLunge(float duration, float speed)
    {
        float timer = 0f;
        RaycastHit closest = default;
        float minDist = Mathf.Infinity;
        Transform target = null;

        RaycastHit[] hits = Physics.SphereCastAll(transform.position, 3f, transform.forward, 3f, enemyLayers);
        foreach(RaycastHit hit in hits)
        {
            if (hit.distance < minDist)
            {
                minDist = hit.distance;
                closest = hit;
            }
        }

        if (lockOnSystem.lockedOn)
        {
            target = lockOnSystem.currentTarget;
        }

        while (timer < duration)
        {
            Vector3 lunge = transform.forward * speed;

            if (!lockOnSystem.lockedOn)
            {
                target = closest.transform;
            }

            if (target != null)
            {
                Vector3 dir = (target.position - transform.position);
                dir.y = 0f;
                Quaternion targetRot = Quaternion.LookRotation(dir);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * speed);
                lunge = dir.normalized * speed;
            }

            Vector3 gravity = Vector3.up * thirdPersonController.verticalVelocity;
            characterController.Move((lunge + gravity) * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }
    }

    public void AllowNextAttack()
    {
        canAttack = true;
    }

    public void ResetCombo()
    {
        canAttack = true;
        comboQueued = false;
        comboIndex = 0;
        isAttacking = false;
    }
}