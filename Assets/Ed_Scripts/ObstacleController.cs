using System.Collections;
using System.Collections.Generic;
using System.Numerics;
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
    private bool pauseObj = false;

    public AudioClip carBeep;
    public AudioClip carStop;
    private AudioSource carSource;
    private float m_spawnTime;

    public void Awake()
    {
        rb = GetComponent<Rigidbody>();

        carSource = GetComponent<AudioSource>();
    }

    public void OnEnable()
    {
        GameManager.onCountDownReached += OnCountDownReached;
        GameManager.onCountDownStarted += OnCountDownRestarted;
        carSource.loop = true;
        carSource.Play();

        m_spawnTime = Time.time;
    }

    public void FixedUpdate()
    {
        // if (Time.time - m_spawnTime > m_aliveTimer)
        // {
        //  TriggerExplosion();
        // return;
        //  }

        if (pauseObj)
            return;

        rb.AddForce((m_obstacleSpeed * GameManager.instance.globalSpeed) * -transform.forward * Time.deltaTime, ForceMode.VelocityChange);
        rb.AddTorque(-rb.angularVelocity * factor);
    }

    /*
    public void OnCollisionEnter(Collision collision)
    {

        if (collision.relativeVelocity.magnitude > explosionMagnitude)
        {
            carSource.pitch = Random.Range(lowPitch, highPitch);
            carSource.PlayOneShot(carExplosionClip);
            Invoke("OnDeath", carExplosionClip.length);
        }
          
    }
    */

    public void OnCountDownRestarted()
    {
       // pauseObj = false;
      //  carSource.loop = true;
      //  carSource.Play();
    }

    public void OnCountDownReached()
    {
        // pauseObj = true;
        SpawnManager.instance.GetExplosion(transform.position);
        gameObject.SetActive(false);
       // carSource.loop = false;
       // carSource.pitch = Random.Range(lowPitch, highPitch);
       // carSource.PlayOneShot(carStop);
      //  rb.velocity = UnityEngine.Vector3.zero;
      //  rb.angularVelocity = UnityEngine.Vector3.zero;
    }

    public void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.instance.AddPoint();
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
        GameManager.onCountDownReached -= OnCountDownReached;
        GameManager.onCountDownStarted -= OnCountDownRestarted;
        rb.velocity = UnityEngine.Vector3.zero;
        rb.angularVelocity = UnityEngine.Vector3.zero;
    }


}
