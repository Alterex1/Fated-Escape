using UnityEngine;

public class Controller : MonoBehaviour
{
    public float speed = 10.0f;
    public float runSpeed = 20.0f;
    public float jumpForce = 2.0f;
    public float mouseSensitivity = 100.0f;
    public Transform cameraTransform;
    private float jumpCounter;
    private bool isJumping = false;
    private int jumpCount = 0;
    private int maxJumpCount = 2; // Set maximum number of jumps

    private Rigidbody rb;
    private float verticalRotation = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        movement = cameraTransform.TransformDirection(movement);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.position += movement * runSpeed * Time.deltaTime;
        }
        else
        {
            transform.position += movement * speed * Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && jumpCount < maxJumpCount)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            isJumping = true;
            jumpCount++;
        }

        float rotateHorizontal = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float rotateVertical = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        transform.Rotate(0, rotateHorizontal, 0);

        verticalRotation -= rotateVertical;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            jumpCount = 0; // Reset jump count when player touches the ground
        }
    }
}
