using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

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

    private Rigidbody rb;

    public AudioClip carBeep;
    public AudioClip carExplosionClip;
    private AudioSource carSource;

    public void Awake()
    {
        rb = GetComponent<Rigidbody>();

        carSource = GetComponent<AudioSource>();
    }
    public void OnEnable()
    {
        carSource.Play();
    }

    public void Update()
    {
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

    public void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
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
        rb.velocity = UnityEngine.Vector3.zero;
        rb.angularVelocity = UnityEngine.Vector3.zero;
    }


}
