using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    [SerializeField]
    private Vector3 iconSetPosition;

    [SerializeField]
    private bool effectSelected = false;

    [SerializeField]
    private float iconSetPositionSpeed = 0.05f;

    [SerializeField]
    private float iconSetScaleSpeed = 0.05f;

    [SerializeField]
    private float iconDecesentAmount = -1.5f;

    [SerializeField]
    private float iconDescentSpeed = 0.05f;

    public void Update()
    {
        if (effectSelected)
        {
            transform.rotation = Quaternion.identity;
            transform.position =  Vector3.Lerp(transform.position, iconSetPosition, iconSetPositionSpeed * Time.deltaTime);
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1, 1f, 1f), iconSetScaleSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(0f, iconDecesentAmount, 0f), iconDescentSpeed) * Time.deltaTime;
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(2, 2, 2), iconSetScaleSpeed * Time.deltaTime);
        }
      

    }

    public void OnTriggerEnter(Collider other)
    { 

        if (other.tag != "Player")
            return;

        if (effectSelected)
            return;

        iconSetPosition = SpawnManager.instance.SetIconSitPosition();

        effectSelected = true;


    }

    public void OnDisable()
    {
        effectSelected = false;
    }
}
