using UnityEngine;

public class DummyEnemy : MonoBehaviour, IDamageable
{
    float health = 100f;

    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject, 1f);
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        Debug.Log("damage taken" + health);
    }
}
