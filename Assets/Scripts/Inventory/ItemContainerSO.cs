using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Inventory/Container")]
public class InventoryContainerSO : ScriptableObject
{
    public int gridWidth = 10;
    public int gridHeight = 6;

    [System.Serializable]
    public class ItemEntry
    {
        public InventoryItemSO itemSO;
        public Vector2Int position; // Where it's placed in the grid
    }

    public List<ItemEntry> items = new List<ItemEntry>();
}
