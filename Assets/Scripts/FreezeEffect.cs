using UnityEngine;

public class FreezeEffect : StatusEffect
{
    private float slowMultiplier;
    private GameObject vfxPrefab;
    private GameObject vfxInstance;

    public FreezeEffect(ElementSO elementData)
    {
        this.duration = elementData.statusDuration;
        this.remaining = elementData.statusDuration;
        this.elementType = elementData.elementType;
        this.vfxPrefab = elementData.hitVFX;
    }

     public override void OnApply(IDamageable target)
    {

        MonoBehaviour mono = target as MonoBehaviour;
        vfxInstance = Object.Instantiate(vfxPrefab, mono.transform);
        //hasar ver ve düşman hızını düşür
        //vfxi başlat
    }

    public override void OnTick(IDamageable target, float deltaTime) { }

    public override void OnExpire(IDamageable target)
    {
        // hızı geri ver
        //vfxi kapa
    }
}
