using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCollider : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        other.gameObject.SetActive(false);
    }
}
