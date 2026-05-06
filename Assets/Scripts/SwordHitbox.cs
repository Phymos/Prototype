using UnityEngine;

public class SwordHitbox : MonoBehaviour
{
    private PlayerCombat playerCombat;

    void Start()
    {
        playerCombat = GetComponentInParent<PlayerCombat>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!playerCombat.swordCollider.enabled) return;

        if (((1 << other.gameObject.layer) & playerCombat.enemyLayers) != 0
            && !playerCombat.alreadyHit.Contains(other.gameObject))
        {
            if (other.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(playerCombat.lightAttackDamage);
                playerCombat.alreadyHit.Add(other.gameObject);
                playerCombat.StartCoroutine(playerCombat.HitStop(0.1f));
            }
        }
    }
}
