using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;

    public void SpawnNewItem(ElementSO element, InventorySlot slot)
    {
        GameObject newItem = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItem.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(element);
    }

    public void AddItem(ElementSO element)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                SpawnNewItem(element, slot);
                return;
            }
        }
    }
}
