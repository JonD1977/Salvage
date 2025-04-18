using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class UIInputController : MonoBehaviour
{
    [Header("UI References")]
    public GameObject inventoryPanel;

    private PlayerInputActions inputActions;
    private bool isInventoryOpen = false;

    void Awake()
    {
        inputActions = new PlayerInputActions();
        inputActions.Player.Inventory.performed += ctx => ToggleInventory();
    }

    void OnEnable()
    {
        inputActions.Enable();
    }

    void OnDisable()
    {
        inputActions.Disable();
    }

    void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;
        Debug.Log("Inventory Open: " + isInventoryOpen);
        inventoryPanel.SetActive(isInventoryOpen);

        Cursor.lockState = isInventoryOpen ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isInventoryOpen;

        // Toggle camera control
        var cameraLook = FindAnyObjectByType<CameraLook>();
        if (cameraLook != null)
        {
            cameraLook.canLook = !isInventoryOpen;
            Debug.Log("Can Look set to: " + cameraLook.canLook);
        }
        else
        {
            Debug.LogWarning("CameraLook not found!");
        }

        // Delay movement toggle to avoid physics weirdness
        StartCoroutine(DelayedDisableMovement(isInventoryOpen));
    }

    private IEnumerator DelayedDisableMovement(bool openingInventory)
    {
        yield return new WaitForEndOfFrame();

        var movement = FindAnyObjectByType<MovementController>();
        if (movement != null)
        {
            movement.enabled = !openingInventory;
            Debug.Log("Movement enabled (delayed): " + movement.enabled);

            var rb = movement.GetComponent<Rigidbody>();
            if (rb != null)
            {
                if (openingInventory)
                {
                    rb.linearVelocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                    rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
                    Debug.Log("Rigidbody velocity reset + Y position frozen.");
                }
                else
                {
                    rb.constraints = RigidbodyConstraints.FreezeRotation;
                    Debug.Log("Rigidbody Y position unfrozen.");
                }
            }
        }
        else
        {
            Debug.LogWarning("MovementController not found!");
        }
    }
}
