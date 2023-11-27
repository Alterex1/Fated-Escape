using UnityEngine;

public class Controller : MonoBehaviour
{
    public AmmoManager gunOnDisplay;

    public CharacterController controller;

    public float speed = 15.0f;
    public float runSpeed = 25.0f;
    public float gravity = -15.0f;
    public float jumpForce = 4.0f;
    public Transform groundCheck;
    public float groundDistance = 1.0f;
    public LayerMask groundMask;

    private Vector3 velocity;
    private bool isGrounded;

    private int jumpCount = 0;
    private int extraJumpCount = 1;

    public float weaponInstance;
    public GameObject[] weapons = new GameObject[3];

    public float normalFOV = 60f; // Normal field of view
    public float aimFOV = 45f; // Field of view when aiming
    public float fovSpeed = 2f; // Speed of FOV change
    private Camera cam; // Camera component

    void Update()
    {
        // Player Movement
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded) {
            jumpCount = 0;
            if (velocity.y < 0)
                velocity.y = -5f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        if (Input.GetKey(KeyCode.LeftShift))
            controller.Move(move * runSpeed * Time.deltaTime);
        else
            controller.Move(move * speed * Time.deltaTime);
        
        if (Input.GetButtonDown("Jump") && (isGrounded || jumpCount < extraJumpCount)) {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
            jumpCount++;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (Input.GetKey(KeyCode.Alpha1)) 
        {
            switchWeapons(1);
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            switchWeapons(2);
        }

        if (Input.GetKey(KeyCode.Alpha3))
        {
            switchWeapons(3);
        }
        if (Input.GetMouseButton(1)) // Right mouse button
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, aimFOV, fovSpeed * Time.deltaTime);
        }
        else
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, normalFOV, fovSpeed * Time.deltaTime);
        }

    }

    public void Start()
    {
        switchWeapons(1);
        cam = GetComponentInChildren<Camera>();

    }

    public void switchWeapons(int index)
    {
        Debug.Log("Switching to weapon: " + index);

        for (int i = 0; i < weapons.Length; i++)
        {
            if (i == index - 1)
            {
                weapons[i].SetActive(true);
                Debug.Log("Activating weapon: " + i);
            }
            else
            {
                weapons[i].SetActive(false);
                Debug.Log("Deactivating weapon: " + i);
            }
        }
        weaponInstance = index;
        gunOnDisplay.setWeaponToDisplay(index - 1);
    }

}
