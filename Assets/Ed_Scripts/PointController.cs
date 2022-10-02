using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PointController : MonoBehaviour
{
    public float speed = 1f;

    public Vector3 targetPoint;

    public void Update()
    {
        if(Vector3.Distance(transform.position, targetPoint) > 0.1f)
        transform.position = Vector3.Lerp(transform.position, targetPoint, speed * Time.deltaTime);
        else
            gameObject.SetActive(false);

    }
}
