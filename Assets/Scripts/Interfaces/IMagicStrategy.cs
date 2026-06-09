using UnityEngine;

public interface IMagicStrategy
{
    void ExecuteMagic(Transform casterTransform, ElementSO elementData, LayerMask enemyLayers);
}
