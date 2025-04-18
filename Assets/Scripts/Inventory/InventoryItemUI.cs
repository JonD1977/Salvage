using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItemUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [Header("For editor assignment (e.g. prefabs)")]
    [SerializeField] private InventoryItemSO itemDataEditor;

    private Vector2Int gridPosition;
    private InventoryItemSO itemData;
    private InventoryGridUI parentGrid;
    private Image image;

    public InventoryItemSO ItemData => itemData;
    public Vector2Int GridPosition => gridPosition;

    private void Awake()
    {
        if (image == null)
            image = GetComponent<Image>();

        if (itemDataEditor != null && itemData == null)
        {
            Setup(itemDataEditor, Vector2Int.zero, null);
        }
    }

    public void Setup(InventoryItemSO data, Vector2Int position, InventoryGridUI grid)
    {
        if (image == null)
            image = GetComponent<Image>();

        itemData = data;
        gridPosition = position;
        parentGrid = grid;

        Debug.Log($"✅ Setup item: {itemData.itemName}");

        if (itemData.icon != null)
        {
            image.sprite = itemData.icon;
            image.enabled = true;
        }
        else
        {
            Debug.LogWarning($"⚠️ No icon for item: {itemData.itemName}");
            image.enabled = false;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (itemData != null)
        {
            InventoryCursor.Instance.PickUpItem(itemData);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        InventoryCursor.Instance.TryDropItem();
    }
}
