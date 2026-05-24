using UnityEngine;

public class SwordHitbox : MonoBehaviour
{
    private PlayerCombat playerCombat;
    private PlayerCombatSfx playerCombatSfx;

    void Start()
    {
        playerCombat = GetComponentInParent<PlayerCombat>();
        playerCombatSfx = GetComponentInParent<PlayerCombatSfx>();
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
                playerCombatSfx.PlaySlashSound();
                playerCombat.StartCoroutine(playerCombat.HitStop(0.1f));
            }
        }
    }
}
