using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    private ParticleSystem _particleSystem;

    public void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    public void OnEnable()
    {
        _particleSystem.Play();
        Invoke("Logic", 2f);
    }

    public void Logic()
    {
        gameObject.SetActive(false);
    }

    public void OnDisable()
    {
        _particleSystem.Stop();
    }



}
