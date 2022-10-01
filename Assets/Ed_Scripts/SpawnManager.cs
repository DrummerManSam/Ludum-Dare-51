using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    [SerializeField]
    private ObstacleBase[] obstaclePrefabList;

    [SerializeField]
    private Vector3 spawnCenterPosition;

    public void Awake()
    {
        if(instance != null)
            Destroy(this);
        else
            instance = this;

        for(int i = 0; i < obstaclePrefabList.Length; i++)
        {

            for(int j = 1; j < obstaclePrefabList[i].numberOfObjectToSpawn; j++)
            {
                GameObject tempObj = Instantiate(obstaclePrefabList[i].obstaclePrefab, this.transform);
                obstaclePrefabList[i].obstacleList.Add(tempObj);
                tempObj.SetActive(false);
            }
        }

        spawnCenterPosition.x = Camera.main.transform.position.x;

    }

    public void SpawnObstacle(int obstacleId)
    {
        for (int i = 0; i < obstaclePrefabList[obstacleId].obstacleList.Count; i++)
        {
            if (!obstaclePrefabList[obstacleId].obstacleList[i].activeInHierarchy)
            {
                obstaclePrefabList[obstacleId].obstacleList[i].transform.position = obstaclePrefabList[obstacleId].spawnOffset + spawnCenterPosition + new Vector3(Random.Range(-obstaclePrefabList[obstacleId].spawnRange, obstaclePrefabList[obstacleId].spawnRange + 1), 0f ,0f);
                obstaclePrefabList[obstacleId].obstacleList[i].SetActive(true);
                return;
            }
        }
    }
}
