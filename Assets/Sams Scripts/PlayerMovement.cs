using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    public int speed = 5;

    [SerializeField]
    private Vector2 movement;
    private Rigidbody rb;

    [SerializeField]
    private Vector3 finalMovement;
    
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    public void OnMovement(InputValue input)
    {
        movement = input.Get<Vector2>();

        finalMovement = new Vector3(movement.x, movement.y, 0f) * Time.deltaTime * speed;
    }

    private void FixedUpdate()
    {
        rb.AddForce(finalMovement, ForceMode.Impulse);
    }

}


