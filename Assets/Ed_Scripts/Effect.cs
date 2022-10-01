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
    private string m_description;
    public string description { get { return m_description; } }

    public virtual void InitEffect()
    {

    }


    public virtual void StopEffect()
    {


    }
}
