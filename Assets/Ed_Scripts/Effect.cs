using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    //1: Easy, //10: Crazy
    public int effectRaiting = 1;

    private bool effectStarted = false;
    public virtual void InitEffect()
    {


        effectStarted = true;
    }

    public virtual void TickEffect()
    {
        if (!effectStarted)
            return;
    }

    public virtual void StopEffect()
    {
        effectStarted = false;


    }
}
