using System.Collections.Generic;
using UnityEngine;

public class StatusEffectHandler : MonoBehaviour
{
    private List<StatusEffect> activeEffects = new List<StatusEffect>();
    private IDamageable owner;

    void Awake()
    {
        owner = GetComponent<IDamageable>();
    }

    public void Apply(StatusEffect effect)
    {
        activeEffects.Add(effect);
        effect.OnApply(owner);
    }

    void Update()
    {
        for (int i = activeEffects.Count - 1; i >= 0; i--)
        {
            activeEffects[i].remaining -= Time.deltaTime;
            activeEffects[i].OnTick(owner, Time.deltaTime);

            if (activeEffects[i].remaining <= 0)
            {
                activeEffects[i].OnExpire(owner);
                activeEffects.RemoveAt(i);
            }
        }
    }
}
