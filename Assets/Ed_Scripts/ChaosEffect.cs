using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaosEffect : EffectController
{
    [SerializeField]
    private float speedValue = 5f;

    [SerializeField]
    private float densityValue = 0.5f;
    public override void IntEffect()
    {
        base.IntEffect();

            SpawnManager.instance.currentSpawnOrder = SpawnManager.SpawnOrder.random;
         GameManager.instance.globalSpeed += speedValue;
         GameManager.instance.spawnTimer += densityValue;
    }
}
