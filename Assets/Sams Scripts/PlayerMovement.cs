using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    public int speed = 5;

    [SerializeField]
    private Vector2 playerInput;
    private Rigidbody rb;

    [SerializeField]
    private Vector3 finalMovement;

    private float xAxis;

    public float acceleration = 3.0f;
    public float decceleration = 3.0f;
    public float velPower = 1.5f;

    public float m_playerDamage = 0f;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    public void OnMovement(InputValue input)
    {
        playerInput = input.Get<Vector2>();
        xAxis = playerInput.x;
    }

    public void OnTriggerEnter(Collider other)
    {
      //  m_playerDamage += 1;
      //  rb.AddForce(Vector3.up * m_playerDamage, ForceMode.Impulse);
    }


    private void FixedUpdate()
    {
        float targetSpeed = xAxis * speed;

        float speedDif = targetSpeed - rb.velocity.x;

        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : decceleration;

        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);

        rb.AddForce(movement * Vector2.right);

    }

}


