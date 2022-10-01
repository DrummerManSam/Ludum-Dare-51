using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObstacleBase 
{
    public enum ObstacleDifficulty { easy, medium, hard, insane, impossible };

    [SerializeField]
    private string obstacleName;

    public GameObject obstaclePrefab;

    public int numberOfObjectToSpawn = 10;

    public ObstacleDifficulty objectDifficulty = ObstacleDifficulty.easy;

    public Vector3 spawnOffset;

    public float[] spawnRangeList;

    [HideInInspector]
    public List<GameObject> obstacleList;
}
