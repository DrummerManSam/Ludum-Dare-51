using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    [SerializeField]
    private Vector3 iconSetPosition;

    [SerializeField]
    private bool effectSelected = false;
    public bool EffectSelected { get { return effectSelected; } }

    [SerializeField]
    private float iconSetPositionSpeed = 3f;

    [SerializeField]
    private float iconSetScaleSpeed = 1f;

    [SerializeField]
    private float iconDecesentAmount = 1.5f;

    [SerializeField]
    private float iconDescentSpeed = 0.85f;

    private Vector3 spawnPos;

    public void OnEnable()
    {
        spawnPos = transform.position;
        GameManager.instance.AddEffectToList(this);
    }

    public virtual void IntEffect()
    {
        
    }

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
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, spawnPos.y - iconDecesentAmount, transform.position.z), iconDescentSpeed * Time.deltaTime) ;
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(2, 2, 2), iconSetScaleSpeed * Time.deltaTime);
        }
      
        
    }

    public void SelectedEffect()
    {

        iconSetPosition = SpawnManager.instance.SetIconSitPosition();

        effectSelected = true;

        GameManager.instance.EffectSelected();
    }

    public void OnTriggerEnter(Collider other)
    { 

        if (other.tag != "Player")
            return;

        if (effectSelected)
            return;

        iconSetPosition = SpawnManager.instance.SetIconSitPosition();


        effectSelected = true;


        GameManager.instance.EffectSelected();


    }

    public void OnDisable()
    {
        effectSelected = false;
    }
}
