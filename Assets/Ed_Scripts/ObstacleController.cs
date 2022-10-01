using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    [SerializeField]
    private float m_obstacleSpeed = 1f;

    public void Update()
    {
        transform.position += (m_obstacleSpeed * GameManager.instance.globalSpeed) * -transform.forward * Time.deltaTime;
    }

    public void OnHit()
    {

    }
}
