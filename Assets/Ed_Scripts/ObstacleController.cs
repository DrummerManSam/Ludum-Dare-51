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

    private Rigidbody rb;

    public void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Update()
    {
        rb.AddForce((m_obstacleSpeed * GameManager.instance.globalSpeed) * -transform.forward * Time.deltaTime, ForceMode.VelocityChange);
        rb.AddTorque(-rb.angularVelocity * factor);
    }

    public void OnTriggerEnter(Collider other)
    {
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
