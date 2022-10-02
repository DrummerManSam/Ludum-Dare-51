using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeEffect : EffectController
{
    [SerializeField]
    private float sizeValue = 1f;
    public override void IntEffect()
    {
        base.IntEffect();

        SpawnManager.instance.obstacleSizeAdjuster += sizeValue;
    }
}
