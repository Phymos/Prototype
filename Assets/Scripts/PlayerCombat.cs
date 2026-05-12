using System;
using System.Collections;
using System.Collections.Generic;
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
    private float lastInputTime = -100f;
    [SerializeField] float attackTimer = 0.5f;
    [SerializeField] float comboTimer = 1f;
    private float lastComboTime = 0f;
    private float lastAttackTime = -100f;
    private int comboIndex = 0;

    public Collider swordCollider;
    public List<GameObject> alreadyHit = new List<GameObject>();
    
    public bool isBlocking = false;
    public bool isArmed = false;
    public bool isAttacking = false;

    private CharacterController characterController;
    private ThirdPersonController thirdPersonController;
    private LockOnSystem lockOnSystem;
    
    public static event Action<int> OnLightAttacking;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        thirdPersonController = GetComponent<ThirdPersonController>();
        lockOnSystem = GetComponent<LockOnSystem>();
    }

    void Update()
    {
        if (Time.time - lastComboTime > comboTimer)
        {
            comboIndex = 0;
            OnLightAttacking?.Invoke(comboIndex);
            isAttacking = false;
        }

        if (Time.time - lastInputTime <= attackBufferTime && Time.time - lastAttackTime >= attackTimer)
        {
            PerformLightAttack();
            lastInputTime = -100f;
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

        lastInputTime = Time.time;
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

    void PerformLightAttack()
    {
        lastAttackTime = Time.time;
        lastComboTime = Time.time;
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
}