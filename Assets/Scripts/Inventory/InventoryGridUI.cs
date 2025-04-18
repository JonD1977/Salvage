using System.Collections.Generic;
using UnityEngine;

public class InventoryGridUI : MonoBehaviour
{
    public static InventoryGridUI Instance { get; private set; }

    [Header("Grid Settings")]
    public int width = 10;
    public int height = 6;
    public int cellSize = 64;

    [Header("References")]
    public Transform itemParent;
    public GameObject itemUIPrefab;
    public GameObject slotPrefab;

    private InventoryItemUI[,] grid;

    private void Awake()
    {
        Instance = this;
        grid = new InventoryItemUI[width, height];
        GenerateGridSlots();
    }

    private void GenerateGridSlots()
    {
        if (slotPrefab == null || itemParent == null) return;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject slot = Instantiate(slotPrefab, itemParent);
                RectTransform rt = slot.GetComponent<RectTransform>();
                rt.anchoredPosition = new Vector2(x * cellSize, -y * cellSize);
                rt.sizeDelta = new Vector2(cellSize, cellSize);
            }
        }
    }

    public bool CanPlaceItem(InventoryItemSO item, Vector2Int position)
    {
        for (int x = 0; x < item.Width; x++)
        {
            for (int y = 0; y < item.Height; y++)
            {
                int gridX = position.x + x;
                int gridY = position.y + y;

                if (gridX >= width || gridY >= height || grid[gridX, gridY] != null)
                    return false;
            }
        }
        return true;
    }

    public void SpawnItem(InventoryItemSO item, Vector2Int position)
    {
        if (!CanPlaceItem(item, position)) return;

        GameObject go = Instantiate(itemUIPrefab, itemParent);
        InventoryItemUI itemUI = go.GetComponent<InventoryItemUI>();

        itemUI.Setup(item, position, this);

        RectTransform rt = go.GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(position.x * cellSize, -position.y * cellSize);
        rt.sizeDelta = new Vector2(item.Width * cellSize, item.Height * cellSize);

        for (int x = 0; x < item.Width; x++)
        {
            for (int y = 0; y < item.Height; y++)
            {
                grid[position.x + x, position.y + y] = itemUI;
            }
        }

        Debug.Log($"✅ Spawned {item.ItemName} at {position}");
    }
    public bool TryPlaceItem(InventoryItemSO itemData)
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Vector2Int pos = new Vector2Int(x, y);
                if (CanPlaceItem(itemData, pos))
                {
                    SpawnItem(itemData, pos);
                    Debug.Log($"📦 Item placed: {itemData.itemName} at {pos}");
                    return true;
                }
            }
        }

        Debug.LogWarning($"❌ No space to place item: {itemData.itemName}");
        return false;
    }

}
