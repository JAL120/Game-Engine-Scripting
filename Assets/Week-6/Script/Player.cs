using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    float speed = 5.0f;
    [SerializeField]
    float rotation = 5.0f;
    [SerializeField]
    float jumpForce = 10f;
    [SerializeField] GameObject BulletPrefab;
    [SerializeField] Transform BulletSpawnTransform;

    public PlayerControls playerControls;

    private float mouseDeltaX = 0f;
    private float mouseDeltaY = 0f;
    private float cameraRotX = 0f;
    private int rotDir = 0;
    private bool grounded;

    private InputAction move;
    private InputAction look;
    private InputAction jump;
    private InputAction fire;

    public int maxHealth = 100;
    private int currentHealth;
    public int damageAmount = 10;

    public int keys = 0;
    public int coins = 0;

    public TMP_Text keysText;
    public TMP_Text coinsText;
    public TMP_Text healthText;

    Rigidbody rb;


    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Awake()
    {
        playerControls = new PlayerControls();

        rb = GetComponent<Rigidbody>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        move = playerControls.Player.Move;
        jump = playerControls.Player.Jump;
        look = playerControls.Player.Look;
        fire = playerControls.Player.Fire;
    }

    private void OnEnable()
    {       
        move.Enable();
        look.Enable();

        jump.Enable();
        jump.performed += Jump;

        fire.Enable();
        fire.performed += Fire;
    }

    private void OnDisable()
    {
        move.Disable();
        jump.Disable();
        look.Disable();
        fire.Disable();
    }

    private void Update()
    {
        HandleHorizontalRotation();
        HandleVerticalRotation();
    }

    private void FixedUpdate()
    {
        grounded = IsGrounded();

        HandleMovement();
    }

    void HandleMovement()
    {
        if (grounded == false) return;

        Vector2 axis = move.ReadValue<Vector2>();

        Vector3 input = (axis.x * transform.right) + (transform.forward * axis.y);

        input *= speed;

        rb.velocity = new Vector3(input.x, rb.velocity.y, input.z);
    }

    bool IsGrounded()
    {
        int layerMask = 1 << 3;

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up * -1), out hit, 1.1f, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up * -1) * hit.distance, Color.yellow);
            return true;
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            return false;
        }
    }

    void HandleHorizontalRotation()
    {

        mouseDeltaX = look.ReadValue<Vector2>().x;

        if (mouseDeltaX != 0)
        {
            rotDir = mouseDeltaX > 0 ? 1 : -1;

            transform.eulerAngles += new Vector3(0, rotation * Time.deltaTime * rotDir, 0);
        }
    }

    void HandleVerticalRotation()
    {
        mouseDeltaY = look.ReadValue<Vector2>().y;

        if (mouseDeltaY != 0)
        {
            rotDir = mouseDeltaY > 0 ? -1 : 1;

            cameraRotX += rotation * Time.deltaTime * rotDir;
            cameraRotX = Mathf.Clamp(cameraRotX, -45f, 45f);

            var targetRotation = Quaternion.Euler(Vector3.right * cameraRotX);

            Camera.main.transform.localRotation = targetRotation;
        }
    }

    void Jump(InputAction.CallbackContext context)
    {
        if (grounded == false) return;
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    void Fire(InputAction.CallbackContext context)
    {
        Instantiate(BulletPrefab, transform.position, Camera.main.transform.rotation);
    }

    public void OnCollisionEnter(Collision collision)
    {

        if (collision.transform.tag == "Trap")
        {
            TakeDamage(10);
        }
        else if (collision.transform.tag == "Trap")
        {
            Destroy(collision.gameObject);
            keys++;
            UpdateKeysUI();
        }
        else if (collision.gameObject.CompareTag("Door"))
        {
            //Attempt to open the door if the player has a key
            if (keys > 0)
            {

                keys--;
                UpdateKeysUI();
            }
        }
        else if (collision.gameObject.CompareTag("Coin"))
        {
            //Collect the coin and increase coins in inventory
            Destroy(collision.gameObject);
            coins++;
            UpdateCoinsUI();
        }

        void UpdateKeysUI()
        {
            keysText.text = "Keys: " + keys;
        }

        void UpdateCoinsUI()
        {
            coinsText.text = "Coins: " + coins;
        }

        if (collision.transform.tag == "DoorTrigger")
        {
            if (keys > 0)
            {
                collision.gameObject.GetComponent<DoorTrigger>().Open();
                keys--;
            }
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log("Player took damage");
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {

    }
}
