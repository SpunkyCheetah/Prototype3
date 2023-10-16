using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GooberController : MonoBehaviour
{
    // Movement Variables
    public float moveSpeed;
    public float turnSpeed;
    private float verticalInput;
    private float horizontalInput;

    // Jumping Variables
    private Rigidbody rb;
    public float jumpForce;
    public bool isOnGround;

    // Crouching
    public bool isCrouching;

    // Animation Variables
    private Animator animator;

    // Particles
    public ParticleSystem dustCloud;

    // Start is called before the first frame update
    void Start()
    {
        // Get Components
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        // Particles
        dustCloud.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCrouching)
        {
            // Make player move when WASD is pressed
            verticalInput = Input.GetAxis("Vertical");
            horizontalInput = Input.GetAxis("Horizontal");
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime * verticalInput);
            transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime * horizontalInput);

            // Communicate with walk animation
            animator.SetFloat("verticalInput", Mathf.Abs(verticalInput));

            // Make player jump when Space is used
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isOnGround = false;
                animator.SetBool("isOnGround", false);
            }
        }

        // turn dust cloud on
        if (verticalInput > 0 && !dustCloud.isPlaying)
        {
            dustCloud.Play();
        }
        else if (verticalInput <= 0)
        {
            dustCloud.Stop();
        }

        // Make player crouch when Left Control is pressed
        if (Input.GetKeyDown(KeyCode.LeftControl) && !isCrouching)
        {
            isCrouching = true;
            animator.SetBool("isCrouching", true);
        }
        else if (Input.GetKeyDown(KeyCode.LeftControl) && isCrouching)
        {
            isCrouching = false;
            animator.SetBool("isCrouching", false);
        }

        // Make player kick when E is pressed
        if (Input.GetKeyDown(KeyCode.E))
        {
            animator.SetTrigger("kick");
        }

    }

    // Sets isOnGround to true when touches ground
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            animator.SetBool("isOnGround", true);
        }
    }
}
