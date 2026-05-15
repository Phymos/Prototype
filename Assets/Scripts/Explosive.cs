using UnityEngine;

public class Explosive : MonoBehaviour
{
    public GameObject explosionEffect;
    public float radius = 5f;
    public float force = 700f;
    public float damage = 50f;
    public float impactThreshold = 5f;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude >= impactThreshold)
            Explode();
    }

    void Explode()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearbyObjects in colliders)
        {
            if (nearbyObjects.TryGetComponent<IDamageable>(out var damageable))
                damageable.TakeDamage(damage);

            if (nearbyObjects.TryGetComponent<Rigidbody>(out var rb))
                rb.AddExplosionForce(force, transform.position, radius);
        }

        Destroy(gameObject);
    }
}
