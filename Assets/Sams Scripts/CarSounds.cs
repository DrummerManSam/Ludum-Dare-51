using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSounds : MonoBehaviour
{


    public AudioClip carBeep;
    private AudioSource carSource;

    public void Start()
    {
        
        carSource = GetComponent<AudioSource>();
    }


    private void OnTriggerEnter(Collider player)
    {
        if(player.gameObject.CompareTag("Player"))
            {
                carSource.PlayOneShot(carBeep);
                print("This is working");
            }
    }






}
