public class FreezeEffect : StatusEffect
{
    private float slowMultiplier;

    public FreezeEffect(float duration, float slowMultiplier)
    {
        this.duration = duration;
        this.remaining = duration;
        this.elementType = ElementType.Ice;
        this.slowMultiplier = slowMultiplier;
    }

     public override void OnApply(IDamageable target)
    {
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
