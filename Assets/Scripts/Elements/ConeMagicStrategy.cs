using UnityEngine;

[CreateAssetMenu(fileName = "ConeMagicStrategy", menuName = "Scriptable Objects/ConeMagicStrategy")]
public class ConeMagicStrategy : ScriptableObject, IMagicStrategy
{
    public float radius = 5f;
    public float angle = 60f;
    public float forceAmount = 10f;

    public void ExecuteMagic(Transform casterTransform, ElementSO elementData, LayerMask enemyLayers)
    {
        Collider[] hits = Physics.OverlapSphere(casterTransform.position, radius, enemyLayers);
        foreach (Collider col in hits)
        {
            Vector3 dir = (col.transform.position - casterTransform.position).normalized;
            if (Vector3.Angle(casterTransform.forward, dir) < angle / 2f)
            {
                if (col.TryGetComponent<IDamageable>(out var damageable))
                {
                    damageable.TakeDamage(elementData.baseDamage);

                    if (col.TryGetComponent<Rigidbody>(out var rb))
                        rb.AddForce(dir * forceAmount, ForceMode.Impulse);
                }
            }
        }
    }
}
