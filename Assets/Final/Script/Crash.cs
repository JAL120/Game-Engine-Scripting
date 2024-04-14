using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Crash : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 8f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public int orangesCollected = 0;
    public Text orangecounter;

    private Rigidbody rb;
    private bool isGrounded;
    private bool isRunning = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (isRunning)
        {
            // Check if the player is grounded
            isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, 0.1f, groundLayer);

            // Player movement
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            Vector3 moveDirection = new Vector3(horizontalInput, 0f, 1f).normalized;
            rb.velocity = new Vector3(moveDirection.x * moveSpeed, rb.velocity.y, moveDirection.z * moveSpeed);

            // Player jump
            if (isGrounded && Input.GetKeyDown(KeyCode.Space))
            {
                rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boulder"))
        {
            Debug.Log("Game Over - Boulder caught you!");
            isRunning = false;
        }
        else if (other.CompareTag("Orange"))
        {
            Destroy(other.gameObject); // Remove the orange from the scene
            orangesCollected++; // Increase the oranges collected counter
            Debug.Log("Oranges Collected: " + orangesCollected);
            UpdateorangesCollectedUI();
        }
    }

    private void UpdateorangesCollectedUI()
    {
        if (orangecounter != null)
        {
            orangecounter.text = "Oranges: " + orangesCollected.ToString();
        }
    }
}
