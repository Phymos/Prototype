using UnityEngine;

[CreateAssetMenu(fileName = "ElementSO", menuName = "Scriptable Objects/ElementSO")]
public class ElementSO : ScriptableObject
{
    [Header("Information")]
    public string elementName;
    public Sprite icon;
    public Color color;

    [Header("Damage")]
    public float baseDamage;
    public ElementType elementType;

    [Header("Status Effect")]
    public float statusChance;
    public float statusDuration;

    [Header("Visuals")]
    public GameObject attackVFX;
    public GameObject hitVFX;
    public AudioClip hitSFX;
}
