using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    public ElementSO element;
    public Image image;

    public void InitialiseItem(ElementSO newElement)
    {
        element = newElement;
        image.sprite = newElement.icon;
    }
}