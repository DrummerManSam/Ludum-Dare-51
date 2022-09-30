using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    public int speed = 5;
    private Vector2 movement;
    private Rigidbody rb;
    
    
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    public void OnMovement(InputValue input)
    {
        movement = input.Get<Vector2>();
        
          
    }

    private void FixedUpdate()
    {
        //Vector3 moveInput = new Vector3(movement.x, 0, 0);
        rb.MovePosition(rb.position + new Vector3(movement.x, movement.y, 0) * speed * Time.fixedDeltaTime);
    }

}


