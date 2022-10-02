using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficJamEfffect : EffectController
{

    [SerializeField]
    private float speedValue = 1f;

    [SerializeField]
    private float densityValue = 0.25f;
    public override void IntEffect()
    {
        base.IntEffect();

        SpawnManager.instance.currentSpawnOrder = SpawnManager.SpawnOrder.linear;
        GameManager.instance.globalSpeed -= speedValue;
        GameManager.instance.spawnTimer += densityValue;
    }
}
