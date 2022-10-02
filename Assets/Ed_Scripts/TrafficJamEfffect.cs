using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficJamEfffect : EffectController
{

    [SerializeField]
    private float densityValue = 0.25f;
    public override void IntEffect()
    {
        base.IntEffect();

        SpawnManager.instance.currentSpawnOrder = SpawnManager.SpawnOrder.Jam;
        GameManager.instance.spawnTimer += densityValue;
    }
}
