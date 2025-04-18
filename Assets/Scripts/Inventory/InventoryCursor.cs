using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryCursor : MonoBehaviour
{
    public GameObject dragIconPrefab;
    private GameObject dragIcon;
    private Image dragIconImage;
    private InventoryItemSO currentItem;

    private PlayerInputActions input;

    public static InventoryCursor Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        // Create drag icon instance
        dragIcon = Instantiate(dragIconPrefab, transform);
        dragIconImage = dragIcon.GetComponent<Image>();

        if (dragIconImage == null)
        {
            Debug.LogError("❌ Drag icon prefab is missing an Image component.");
        }

        dragIcon.SetActive(false);

        // Setup input system
        input = new PlayerInputActions();
        input.Player.Enable();

        // 🔥 Make sure you have a "Click" action bound to <Mouse>/leftButton in the Input Actions file
        input.Player.Click.performed += ctx => TryDropItem();
    }

    private void OnDisable()
    {
        input.Player.Disable(); // Prevent memory leaks
    }

    private void Update()
    {
        if (dragIcon.activeSelf)
        {
            // Make the icon follow the mouse
            dragIcon.transform.position = Mouse.current.position.ReadValue();
        }
    }

    public void PickUpItem(InventoryItemSO item)
    {
        currentItem = item;

        if (item == null)
        {
            Debug.LogWarning("⚠️ Tried to pick up a null item.");
            return;
        }

        if (item.icon == null)
        {
            Debug.LogWarning($"⚠️ Item '{item.name}' has no icon assigned!");
        }
        else
        {
            dragIconImage.sprite = item.icon;
            dragIconImage.color = Color.white;
            dragIconImage.preserveAspect = true;
            dragIconImage.enabled = true;

            Debug.Log($"✅ Sprite assigned: {item.icon.name}");
        }

        dragIcon.SetActive(true);
        Debug.Log($"Picked up: {item.name}");
    }

    public void DropItem()
    {
        if (currentItem == null)
        {
            Debug.LogWarning("⚠️ Tried to drop an item, but none is held.");
            return;
        }

        Debug.Log("🪂 Dropped item.");
        currentItem = null;
        dragIcon.SetActive(false);
    }

    public void TryDropItem()
    {
        if (dragIcon.activeSelf)
        {
            Debug.Log("💥 Drop triggered by input.");
            DropItem();
        }
    }

    public InventoryItemSO GetHeldItem()
    {
        return currentItem;
    }
}
