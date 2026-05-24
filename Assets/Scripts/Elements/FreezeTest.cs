using UnityEngine;

public class FreezeTest : MonoBehaviour
{
    public ElementSO elementData;

    void Start()
    {
        GetComponent<StatusEffectHandler>().Apply(new FreezeEffect(elementData));
    }
}
