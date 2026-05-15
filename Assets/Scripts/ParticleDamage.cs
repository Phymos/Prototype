using System.Collections.Generic;
using UnityEngine;

public class ParticleDamage : MonoBehaviour
{
    public float damage = 10f;
    public ElementSO elementData;
    
    private ParticleSystem ps;
    private List<ParticleCollisionEvent> collisionEvents = new();

    void Start() => ps = GetComponent<ParticleSystem>();

    void OnParticleCollision(GameObject other)
    {
        ps.GetCollisionEvents(other, collisionEvents);

        if (other.TryGetComponent<IDamageable>(out var damageable))
            damageable.TakeDamage(damage);

        if (elementData != null && other.TryGetComponent<StatusEffectHandler>(out var handler))
            handler.Apply(new FreezeEffect(elementData));
    }
}
