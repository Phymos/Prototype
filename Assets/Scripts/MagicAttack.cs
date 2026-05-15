using UnityEngine;
using UnityEngine.InputSystem;

public class MagicAttack : MonoBehaviour
{

    public void CastCone(float radius, float angle, float damage, float forceAmount, LayerMask enemyLayers)
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, enemyLayers);
        foreach (Collider col in hits)
        {
            Vector3 dir = (col.transform.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dir) < angle / 2f)
            {
                if (col.TryGetComponent<IDamageable>(out var damageable))
                {
                    damageable.TakeDamage(damage);
                    
                    if (col.TryGetComponent<Rigidbody>(out var rb))
                        rb.AddForce(dir * forceAmount, ForceMode.Impulse);
                }
            }
        }
    }
}
