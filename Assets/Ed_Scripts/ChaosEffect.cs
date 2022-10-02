using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaosEffect : EffectController
{
    public float chasoValue = 1f;

    public override void IntEffect()
    {
        base.IntEffect();

        GameManager.instance.chaosFactor += chasoValue;
    }
}
