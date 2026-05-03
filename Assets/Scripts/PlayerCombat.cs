using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    [Header("References")]
    public Transform attackPoint;
    public LayerMask enemyLayers;

    [Header("Attack Settings")]
    public float attackRange = 0.5f;
    public float lightAttackDamage = 20f;
    public float heavyAttackDamage = 40f;
    
    public bool isBlocking = false;
    public bool isArmed = false;


    public float attackBufferTime = 0.5f;
    private float lastInputTime = -100f;
    [SerializeField] float attackTimer = 0.5f;
    [SerializeField] float comboTimer = 1f;
    private float lastComboTime = 0f;
    private float lastAttackTime = -100f;
    private int comboIndex = 0;
    public static event Action<int> OnLightAttacking;

    void Update()
    {
        if (Time.time - lastComboTime > comboTimer)
        {
            comboIndex = 0;
            OnLightAttacking?.Invoke(comboIndex);
        }

        if (Time.time - lastInputTime <= attackBufferTime && Time.time - lastAttackTime >= attackTimer)
        {
            PerformLightAttack();
            lastInputTime = -100f;
        }
    }

    public void OnLightAttack(InputAction.CallbackContext context)
    {
        if (!context.performed || !isArmed) return;

        lastInputTime = Time.time;
    }

    void OnHeavyAttack(InputAction.CallbackContext context)
    {
        if (!isArmed) return;
        
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider enemy in hitEnemies)
        {
            if (enemy.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(heavyAttackDamage);
            }
        }
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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    void PerformLightAttack()
{
    lastAttackTime = Time.time;
    lastComboTime = Time.time;
    comboIndex++;

    if (comboIndex > 3) comboIndex = 1;

    OnLightAttacking?.Invoke(comboIndex);

    Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);
    foreach (Collider enemy in hitEnemies)
    {
        if (enemy.TryGetComponent(out IDamageable damageable))
            damageable.TakeDamage(lightAttackDamage);
    }
}
}