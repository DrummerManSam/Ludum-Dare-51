using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSounds : MonoBehaviour
{


    public AudioClip carBeep;

    public void Start()
    {
        GetComponent<AudioSource>().clip = carBeep;
    }


    private void OnTriggerEnter(Collider player)
    {
        if(player.gameObject.CompareTag("Player"))
            {
                GetComponent<AudioSource>().Play();
            }
    }






}
