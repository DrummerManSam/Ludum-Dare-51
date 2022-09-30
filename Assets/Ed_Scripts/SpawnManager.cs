using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
   public static SpawnManager instance;

    [SerializeField]
    private int numberOfObstaclesToSpawn;




    public void Awake()
    {
        if(instance != null)
            Destroy(this);
        else
            instance = this;




    }
}
