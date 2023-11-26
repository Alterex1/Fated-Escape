using UnityEngine;
using System.Collections.Generic;

public class Controller : MonoBehaviour
{
    public CharacterController controller;

    // Audio
    public List<AudioClip> footstepSounds;
    public AudioClip jumpSound;
    public AudioSource footsteps;


    public float speed = 15.0f, runSpeed = 25.0f, gravity = -15.0f, jumpForce = 4.0f, groundDistance = 1.0f;
    public Transform groundCheck;
    public LayerMask groundMask;

    private Vector3 velocity;
    private bool isGrounded;

    private int jumpCount = 0, extraJumpCount = 1;

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

        if ((x != 0 || z != 0) && !footsteps.isPlaying && isGrounded)
            PlayFootsteps();
        else if (x == 0 && z == 0)
            footsteps.Stop();

        Vector3 move = transform.right * x + transform.forward * z;
        if (Input.GetKey(KeyCode.LeftShift))
            controller.Move(move * runSpeed * Time.deltaTime);
        else
            controller.Move(move * speed * Time.deltaTime);
        
        if (Input.GetButtonDown("Jump") && (isGrounded || jumpCount < extraJumpCount)) {
            footsteps.Stop();
            if (isGrounded)
                footsteps.PlayOneShot(jumpSound, Random.Range(0.5f, 1f));
            
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
            jumpCount++;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void PlayFootsteps()
    {
        if (Input.GetKey(KeyCode.LeftShift))
            footsteps.pitch = Random.Range(1.45f, 1.55f);
        else
            footsteps.pitch = Random.Range(0.95f, 1.05f);

        footsteps.PlayOneShot(footstepSounds[Random.Range(0, footstepSounds.Count)], Random.Range(0.5f, 1f));
    }
}
