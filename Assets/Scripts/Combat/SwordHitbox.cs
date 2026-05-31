using Unity.Cinemachine;
using UnityEngine;

public class SwordHitbox : MonoBehaviour
{
    private PlayerCombat playerCombat;
    private PlayerCombatSfx playerCombatSfx;
    [SerializeField] CinemachineImpulseSource impulseSource;


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
                impulseSource.GenerateImpulse();
                playerCombat.StartCoroutine(playerCombat.HitStop(0.05f));
            }
        }
    }
}
