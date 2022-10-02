using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrucksEffect : EffectController
{
    public override void IntEffect()
    {
        base.IntEffect();

        SpawnManager.instance.trucksAllowed = true;
    }
}
