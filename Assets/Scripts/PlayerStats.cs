using System;
using NUnit.Framework;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IDamageable
{
    public float maxHealth = 100;
    public float currentHealth;
    public float maxMana = 3;
    public float currentMana;
    public float armor;

    public event Action OnDeath;

    

    void Start()
    {
        currentHealth = maxHealth;
        currentMana = maxMana;
    }

    public void UseMana(float amount)
    {
        if (currentMana >= amount)
        {
            currentMana -= amount;
        }
        else
        {
            Debug.Log("Not enough mana!");
        }
    }

    public void TakeDamage(float damage)
    {
        if (GetComponent<PlayerCombat>().isBlocking)
            damage *= 0.5f;

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        OnDeath?.Invoke();
    }
}
