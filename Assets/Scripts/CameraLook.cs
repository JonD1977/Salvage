using UnityEngine;

public class CameraLook : MonoBehaviour
{
    public float sensitivity = 2f;
    public bool canLook = true;

    [SerializeField] private Transform cameraTransform; // Assign in inspector
    [SerializeField] private Transform playerBody;      // Assign in inspector

    private float xRotation = 0f;
    private bool lastLookState = true;

    void Update()
    {
        if (!canLook)
        {
            if (lastLookState)
            {
                Debug.Log("CameraLook is now DISABLED.");
                lastLookState = false;
            }
            return;
        }

        if (!lastLookState)
        {
            Debug.Log("CameraLook is now ACTIVE.");
            lastLookState = true;
        }

        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        playerBody.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
