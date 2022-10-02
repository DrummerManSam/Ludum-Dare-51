using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    [SerializeField]
    private bool isDead = false;
    public bool IsDead { get { return isDead; } }

    private Animator m_animator;
    private SpriteRenderer m_sprite;
    private BoxCollider boxCollider;
    private AudioSource aSource;

    [SerializeField]
    private AudioClip[] deathClip;

    [SerializeField]
    private RigidbodyConstraints m_aliveConstraints;

    [SerializeField]
    private RigidbodyConstraints m_deathConstraints;

    [SerializeField]
    private PhysicMaterial m_deathMaterial;

    [SerializeField]
    private PhysicMaterial m_aliveMaterial;

    private Vector3 startPos;
    private Quaternion startRos;

    private KeyCode restartGame = KeyCode.R;
    private bool isDone = false;


    private void Awake()
    {
        if (instance != null)
            Destroy(instance);

        instance = this;

        rb = GetComponent<Rigidbody>();
        m_animator = GetComponent<Animator>();
        m_sprite = GetComponentInChildren<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider>();
        aSource = GetComponent<AudioSource>();
        startPos = transform.position;
        startRos = transform.rotation;
        m_aliveConstraints = rb.constraints;
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
        aSource.pitch = Random.Range(0.9f, 1.1f);
        aSource.PlayOneShot(deathClip[Random.Range(0, deathClip.Length)]);
        boxCollider.material = m_deathMaterial;
        rb.constraints = m_deathConstraints;
        rb.AddForce(playerDamage * collision.impulse, ForceMode.Impulse);
        m_animator.SetBool("Death", true);
    }

    public void ResetPlayer()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.position = startPos;
        rb.rotation = startRos;
        boxCollider.material = m_aliveMaterial;
        rb.constraints = m_aliveConstraints;
        m_animator.SetBool("Death", false);
        isDead = false;

    }

    public void ResetTheGame()
    {
        SceneManager.LoadScene(0);
    }

    public void Update()
    {
       if(Input.GetKeyDown(restartGame))
        {
            isDone = false;
            if(!isDone)
            {
                ResetTheGame();
            }
        }
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


