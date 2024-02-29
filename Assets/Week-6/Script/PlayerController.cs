using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] InputAction moveaction;
    InputAction jumpaction;
    PlayerControllerMapping mappings;
    const float SPEED = 5.5f;
    Rigidbody rb;
    [SerializeField] float jumpForce = 5f;

    private void Awake()
    {
        mappings = new PlayerControllerMapping();
        rb = GetComponent<Rigidbody>();
        
    }

    bool IsGrounded()
    {
        int layerMask = 1 << 3;
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up * -1), out hit, 1.1f, layerMask)) 
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

    public void OnEnable()
    {
        moveaction = mappings.Player.Move;
        moveaction.Enable();
        jumpaction = mappings.Player.Jump;
        jumpaction.Enable();
        jumpaction.performed += Jump;
    }

    public void OnDisable()
    {
        moveaction.Disable();
        jumpaction.Disable();
        jumpaction.performed -= Jump;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 input = moveaction.ReadValue<Vector2>();
        input *= SPEED * Time.deltaTime;
        rb.velocity = new Vector3(input.x, rb.velocity.y, input.y);
        if (IsGrounded() == false) return;

        //transform.position = new Vector3 (transform.position.x + input.x,transform.position.y, transform.position.z + input.y);
    }

    void Jump(InputAction.CallbackContext context)
    {
        if (IsGrounded() == false) return;
        rb.AddForce(Vector3.up * jumpForce);
    }
}
