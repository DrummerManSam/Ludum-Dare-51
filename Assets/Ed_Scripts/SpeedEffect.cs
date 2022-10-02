using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedEffect : EffectController
{

    [SerializeField]
    private float speedValue = 2f;
    public override void IntEffect()
    {
        base.IntEffect();

        GameManager.instance.globalSpeed += speedValue;
    }

}
