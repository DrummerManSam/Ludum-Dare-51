using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HommingEffect : EffectController
{
    public override void IntEffect()
    {
        base.IntEffect();

        GameManager.instance.isHomming = true;
    }
}
