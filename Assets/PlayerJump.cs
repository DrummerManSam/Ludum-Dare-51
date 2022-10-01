using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public Vector3 jump;
    public float jumpForce = 0.5f;
    public bool isGrounded;
    public bool isJumping;
    public float jumpCutMultiplier = 0.5f;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);
    }

    void OnCollisionStay()
    {
        isGrounded = true;
        isJumping = false;
    }
    void OnCollisionExit()
    {
        isGrounded = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            isJumping = true;


        }

        if (isJumping)
        {
           OnJumpUp();
        }
    }

    public void OnJumpUp()
    {
        if(rb.velocity.y > 0 && isJumping)
        {
            rb.AddForce(Vector3.down * rb.velocity.y * (1 - jumpCutMultiplier), ForceMode.Impulse);
        }
    }
}
