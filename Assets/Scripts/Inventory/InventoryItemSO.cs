using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Item")]
public class InventoryItemSO : ScriptableObject
{
    [Header("Basic Info")]
    public string itemName;
    public Sprite icon;

    [Header("Inventory Size (in cells)")]
    public int width = 1;
    public int height = 1;

    // Optional: Create properties to match the names used in other scripts
    public string ItemName => itemName;
    public Sprite Icon => icon;
    public int Width => width;
    public int Height => height;
}
