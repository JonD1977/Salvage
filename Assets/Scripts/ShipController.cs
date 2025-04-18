using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ShipController : MonoBehaviour
{
    public float thrustForce = 20f;
    public float turnSpeed = 60f;

    private Rigidbody rb;
    private Vector2 moveInput;
    private Vector2 lookInput;

    public bool isControlled = false; // 👈 only accept input if true

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void SetMovementInput(Vector2 input)
    {
        if (isControlled) moveInput = input;
    }

    public void SetLookInput(Vector2 input)
    {
        if (isControlled) lookInput = input;
    }

    private void FixedUpdate()
    {
        if (!isControlled) return;

        Vector3 move = transform.forward * moveInput.y + transform.right * moveInput.x;
        rb.AddForce(move * thrustForce);

        float yaw = lookInput.x * turnSpeed * Time.fixedDeltaTime;
        float pitch = -lookInput.y * turnSpeed * Time.fixedDeltaTime;

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);
        rb.MoveRotation(rb.rotation * rotation);
    }
}
