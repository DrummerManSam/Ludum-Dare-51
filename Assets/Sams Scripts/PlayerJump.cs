using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public Vector3 jump;
    public float jumpForce = 0.5f;
    public bool isGrounded;
    public bool isJumping;
    public bool canJump;
    public float jumpCutMultiplier = 0.5f;
    Rigidbody rb;

    private Animator m_animator;

    [SerializeField]
    private float rayCastDownDepth = 0.1f;

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);
    }

    void OnCollisionStay()
    {
       // isGrounded = true;
      //  isJumping = false;
    }
    void OnCollisionExit()
    {
      //  isGrounded = false;
    }
    void Update()
    {
        isGrounded = IsGrounded();

        if (rb.velocity.y <= 0)
            canJump = true;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && canJump)
        {
            canJump = false;
            isJumping = true;
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            m_animator.SetTrigger("Jump");   
        }

        if (isJumping)
        {
           OnJumpUp();
        }
    }

    public bool IsGrounded()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down, out hit, rayCastDownDepth))
        {
            isJumping = false;
            return true;
        }
        else 
            return false;
    }

    public void OnJumpUp()
    {
        if(rb.velocity.y > 0 && isJumping)
        {
            rb.AddForce(Vector3.down * rb.velocity.y * (1 - jumpCutMultiplier), ForceMode.Impulse);
        }
    }
}
