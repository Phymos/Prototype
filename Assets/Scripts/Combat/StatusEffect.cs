
public abstract class StatusEffect
{
    public ElementType elementType;
    public float duration;
    public float remaining;

    public abstract void OnApply(IDamageable target);
    public abstract void OnTick(IDamageable target, float deltaTime);
    public abstract void OnExpire(IDamageable target);
}
