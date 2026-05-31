using System.Collections;
using UnityEngine;

public class DummyEnemy : MonoBehaviour, IDamageable
{
    float health = 100f;
    [SerializeField] Renderer[] renderers;
    private MaterialPropertyBlock propBlock;

    void Start()
    {
        propBlock = new MaterialPropertyBlock();
    }

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
        StartCoroutine(FlashHit());
    }

    IEnumerator FlashHit()
    {
        SetFlash(1f);
        yield return new WaitForSeconds(0.1f);
        SetFlash(0f);
    }

    void SetFlash(float amount)
    {
        foreach (var r in renderers)
        {
            r.GetPropertyBlock(propBlock);
            propBlock.SetFloat("_HitAmount", amount);
            r.SetPropertyBlock(propBlock);
        }
    }
}
