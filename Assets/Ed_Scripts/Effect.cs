using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    //1: Easy, //10: Crazy
    [SerializeField]
    private int m_effectRating = 1;
    public int effectRating { get { return m_effectRating; } }

    [SerializeField]
    private float m_effectMultiplier = 0.25f;
    public float effectMultiplier { get { return m_effectMultiplier; } }

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
