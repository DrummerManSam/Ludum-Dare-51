using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;
using UnityEngine;
using static GameManager;

public class ObstacleController : MonoBehaviour
{
    [SerializeField]
    private float m_obstacleSpeed = 1f;

    [SerializeField]
    private float factor = 1;

    [SerializeField]
    private float deathTime = 1f;

    [SerializeField]
    private float lowPitch = 0.9f;
    [SerializeField]
    private float highPitch = 1.1f;

    public float explosionMagnitude = 5f;

    [SerializeField]
    private float m_aliveTimer = 10f;

    private Rigidbody rb;

    public AudioClip carBeep;
    public AudioClip carStop;
    private AudioSource carSource;
    private float m_spawnTime;

    private bool nearMissPointGiven = false;
    private Vector3 m_finalDirection;
    private Vector3 m_torque;
    private List<BoxCollider> colliders = new List<BoxCollider>();

    [SerializeField]
    private PhysicMaterial bouncyMat;

    public void Awake()
    {
        rb = GetComponent<Rigidbody>();

        colliders.AddRange(GetComponents<BoxCollider>());

        carSource = GetComponent<AudioSource>();
    }

    public void OnEnable()
    {
        GameManager.onCountDownReached += OnCountDownReached;
        GameManager.onCountDownStarted += OnCountDownRestarted;
        carSource.loop = true;
        carSource.Play();

        m_finalDirection = -transform.forward;
        m_torque = Vector3.zero;

        if (GameManager.instance.chaosFactor != 0)
        {
          //  rb.useGravity = false;
            for(int i = 0; i < colliders.Count; i++)
            {
                colliders[i].material = bouncyMat;
            }
          //  rb.useGravity = false;
           // m_finalDirection = -transform.forward + new Vector3(0f, Random.Range(0, 0.1f), 0f);
            m_torque = new Vector3(0f, 0f, Random.Range(0, GameManager.instance.chaosFactor));
        }

        m_spawnTime = Time.time;
    }

    public void OnCollisionEnter(Collision collision)
    {

        if (GameManager.instance.chaosFactor == 0)
            return;

        rb.AddForce((new Vector3(Random.Range(0, 0.1f), Random.Range(0, 0.1f), -transform.forward.z)) * GameManager.instance.chaosFactor, ForceMode.Impulse);
    }

    public void FixedUpdate()
    {

        rb.AddForce((m_obstacleSpeed * GameManager.instance.globalSpeed) * m_finalDirection * Time.deltaTime, ForceMode.VelocityChange);
        rb.AddTorque(-rb.angularVelocity * factor + m_torque);
    }


    public void OnCountDownRestarted()
    {
    }

    public void OnCountDownReached()
    {
        SpawnManager.instance.GetExplosion(transform.position);
        gameObject.SetActive(false);

    }

    public void OnTriggerEnter(Collider other)
    {

        if (!PlayerMovement.instance.IsDead && other.gameObject.CompareTag("Player") && !nearMissPointGiven)
        {
            nearMissPointGiven = true;
            GameManager.instance.AddPoint();
            SpawnManager.instance.GetNearMissPoint(other.transform.position);
            carSource.pitch = Random.Range(lowPitch, highPitch);
            carSource.PlayOneShot(carBeep);
        }

        if (other.gameObject.tag == "DeathBarrier")
            Invoke("OnDeath", deathTime);
    }

    public void OnDeath()
    {
        gameObject.SetActive(false);
    }

    public void OnDisable()
    {
        nearMissPointGiven = false;
        GameManager.onCountDownReached -= OnCountDownReached;
        GameManager.onCountDownStarted -= OnCountDownRestarted;
        rb.velocity = UnityEngine.Vector3.zero;
        rb.angularVelocity = UnityEngine.Vector3.zero;
    }


}
