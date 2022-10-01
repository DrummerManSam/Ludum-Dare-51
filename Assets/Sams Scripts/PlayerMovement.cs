using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    [SerializeField]
    public int speed = 5;

    public Vector2 playerInput;

    private Rigidbody rb;

    [SerializeField]
    private Vector3 finalMovement;

    private float xAxis;

    public float acceleration = 3.0f;
    public float decceleration = 3.0f;
    public float velPower = 1.5f;

    public float playerDamage = 1f;
    private bool isDead = false;

    private Animator m_animator;
    private SpriteRenderer m_sprite;
    private BoxCollider boxCollider;

    [SerializeField]
    private RigidbodyConstraints m_aliveConstraints;

    [SerializeField]
    private RigidbodyConstraints m_deathConstraints;

    [SerializeField]
    private PhysicMaterial m_deathMaterial;

    private void Awake()
    {
        if(instance == null)
            instance = this;

        rb = GetComponent<Rigidbody>();
        m_animator = GetComponent<Animator>();
        m_sprite = GetComponentInChildren<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider>();

    }

    public void OnMovement(InputValue input)
    {
        playerInput = input.Get<Vector2>();
        xAxis = playerInput.x;
    }

    public void OnCollisionEnter(Collision collision)
    {

        if (collision.transform.tag != "Obstacle")
            return;

        

        isDead = true;

        boxCollider.material = m_deathMaterial;
        rb.constraints = m_deathConstraints;
        rb.AddForce(playerDamage * collision.impulse, ForceMode.Impulse);
        print(playerDamage * collision.impulse.magnitude);
        m_animator.SetBool("Death", true);
    }



    private void FixedUpdate()
    {
        if (isDead)
            return;

        float targetSpeed = xAxis * speed;

        float speedDif = targetSpeed - rb.velocity.x;

        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : decceleration;

        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);

        rb.AddForce(movement * Vector2.right);

        if (rb.velocity.x > 0)
            m_sprite.flipX = false;
        else if(rb.velocity.x <0)
            m_sprite.flipX = true;

        m_animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
    }

   

}


