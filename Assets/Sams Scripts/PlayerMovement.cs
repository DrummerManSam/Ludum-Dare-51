using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private int speed = 5;
    private Vector2 movement;
    private Rigidbody rb;
    private Animator animator;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void OnMovement(InputValue input)
    {
        movement = input.Get<Vector3>();

        /*animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);*/
        
    }

    private void FixedUpdate()
    {
        Vector3 moveInput = new Vector3(movement.x, 0, 0);
        rb.MovePosition(transform.position + moveInput  * speed * Time.fixedDeltaTime);
    }

}

