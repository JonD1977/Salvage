using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private InventoryItemSO itemData;

    public void OnPickup()
    {
        if (InventoryGridUI.Instance.TryPlaceItem(itemData))
        {
            Debug.Log($"✅ Picked up {itemData.itemName}");
            Destroy(gameObject);
        }
        else
        {
            Debug.LogWarning("🚫 Inventory full! Could not pick up item.");
        }
    }
}
