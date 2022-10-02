using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    [SerializeField]
    private ObstacleBase[] obstaclePrefabList;

    public Vector3 spawnCenterPosition;

    [SerializeField]
    private GameObject explosionPrefab;

    private List<GameObject> explosionList = new List<GameObject>();

    [SerializeField]
    private Transform iconSetPosition;

    [SerializeField]
    private float iconSetPositionAdjuster = 0.5f;

    [SerializeField]
    private GameObject[] effectList;

    [SerializeField]
    private Transform effectSpawnPos1;
    [SerializeField]
    private Transform effectSpawnPos2;

    private int iconNumber = 0;

    [SerializeField]
    private GameObject nearMissPointPreab;

    private List<GameObject> nearMissPointsList = new List<GameObject>();

    [SerializeField]
    private Transform nearMissPointSetPos;

    [SerializeField]
    private float m_obstacleSizeAdjuster = 0f;
    public float obstacleSizeAdjuster { get { return m_obstacleSizeAdjuster; } set { m_obstacleSizeAdjuster = value; } }

    public enum SpawnOrder { Linear, Random, Jam};

    public SpawnOrder currentSpawnOrder = SpawnOrder.Linear;

    private int spawnLinearId = 0;

    [SerializeField]
    private Transform spawnCenter;

    [SerializeField]
    private bool m_trucksAllowed = false;
    public bool trucksAllowed { get { return m_trucksAllowed; } set { m_trucksAllowed = value; } }



    public void Awake()
    {
        if (instance != null)
            Destroy(instance);

        instance = this;

        for (int i = 0; i < obstaclePrefabList.Length; i++)
        {

            for(int j = 1; j < obstaclePrefabList[i].numberOfObjectToSpawn; j++)
            {
                GameObject tempObj = Instantiate(obstaclePrefabList[i].obstaclePrefab, this.transform);
                obstaclePrefabList[i].obstacleList.Add(tempObj);
                tempObj.SetActive(false);
            }
        }

        for(int i = 0; i < 20; i++)
        {
            GameObject tempObj = Instantiate(explosionPrefab, this.transform);
            explosionList.Add(tempObj);
            tempObj.SetActive(false);
        }

        for (int i = 0; i < 20; i++)
        {
            GameObject tempObj = Instantiate(nearMissPointPreab, this.transform);
            nearMissPointsList.Add(tempObj);
            tempObj.SetActive(false);
        }

        spawnCenterPosition.x = Camera.main.transform.position.x;

    }

    public void SpawnObstacle(int obstacleId)
    {

        if (trucksAllowed)
            obstacleId = Random.Range(0, obstaclePrefabList.Length);
        else
            obstacleId = 0;

        if (currentSpawnOrder == SpawnOrder.Jam)
        {
            int numberToSpawn = obstaclePrefabList[0].spawnRangeList.Length;
            int tempCounter = 0;

            for (int i = 0; i < obstaclePrefabList[obstacleId].obstacleList.Count; i++)
            {
                if (tempCounter >= numberToSpawn)
                    return;

                if (!obstaclePrefabList[obstacleId].obstacleList[i].activeInHierarchy)
                {
                    
                   
                    obstaclePrefabList[obstacleId].obstacleList[i].transform.position = obstaclePrefabList[obstacleId].spawnOffset + spawnCenterPosition + new Vector3(obstaclePrefabList[obstacleId].spawnRangeList[tempCounter], 0f, 0f);
                    obstaclePrefabList[obstacleId].obstacleList[i].transform.rotation = spawnCenter.rotation;


                    obstaclePrefabList[obstacleId].obstacleList[i].transform.localScale += new Vector3(m_obstacleSizeAdjuster, m_obstacleSizeAdjuster/2, 0f);
                    obstaclePrefabList[obstacleId].obstacleList[i].SetActive(true);
                    tempCounter++;
                }

            }

        }

        for (int i = 0; i < obstaclePrefabList[obstacleId].obstacleList.Count; i++)
        {
            if (!obstaclePrefabList[obstacleId].obstacleList[i].activeInHierarchy)
            {
                int SpawnRangeID = Random.Range(0, obstaclePrefabList[obstacleId].spawnRangeList.Length);

                if(currentSpawnOrder == SpawnOrder.Random)
                {
                    obstaclePrefabList[obstacleId].obstacleList[i].transform.position = obstaclePrefabList[obstacleId].spawnOffset + spawnCenterPosition + new Vector3(obstaclePrefabList[obstacleId].spawnRangeList[SpawnRangeID], 0f, 0f);
                    obstaclePrefabList[obstacleId].obstacleList[i].transform.rotation = spawnCenter.rotation;

                }
               
                else if(currentSpawnOrder == SpawnOrder.Linear)
                {
                      obstaclePrefabList[obstacleId].obstacleList[i].transform.position = obstaclePrefabList[obstacleId].spawnOffset + spawnCenterPosition + new Vector3(obstaclePrefabList[obstacleId].spawnRangeList[spawnLinearId], 0f, 0f);

                      obstaclePrefabList[obstacleId].obstacleList[i].transform.rotation = spawnCenter.rotation;

                    spawnLinearId++;

                    if (spawnLinearId >= obstaclePrefabList[obstacleId].spawnRangeList.Length)
                        spawnLinearId = 0;
                }

                obstaclePrefabList[obstacleId].obstacleList[i].transform.localScale += new Vector3(m_obstacleSizeAdjuster, m_obstacleSizeAdjuster/2, 0f);
                obstaclePrefabList[obstacleId].obstacleList[i].SetActive(true);
                return;
            }
        }
    }

    public void GetExplosion(Vector3 newPosition)
    {
        for(int i = 0; i < explosionList.Count; i++)
        {
            if (!explosionList[i].activeInHierarchy)
            {
                explosionList[i].transform.position = newPosition;
                explosionList[i].SetActive(true);
                return;
            }
        }
    }

    public void GetNearMissPoint(Vector3 newPosition)
    {
        for (int i = 0; i < nearMissPointsList.Count; i++)
        {
            if (!nearMissPointsList[i].activeInHierarchy)
            {
                nearMissPointsList[i].transform.position = newPosition;
                nearMissPointsList[i].GetComponent<PointController>().targetPoint = nearMissPointSetPos.position;
                nearMissPointsList[i].SetActive(true);
                return;
            }
        }
    }

    public void SpawnEffectChoice()
    {
        
        GameObject tempEffect1 = Instantiate(effectList[Random.Range(0, effectList.Length)], effectSpawnPos1.position, effectSpawnPos1.rotation);  
        GameObject tempEffect2 = Instantiate(effectList[Random.Range(0, effectList.Length)], effectSpawnPos2.position, effectSpawnPos2.rotation);    
    }

    public Vector3 SetIconSitPosition()
    {
        Vector3 newPos = iconSetPosition.position + new Vector3((iconSetPositionAdjuster * iconNumber), 0f, 0f);
        iconNumber++;
        return newPos;
    }

    public void ResetAllObstacles()
    {
        for (int i = 0; i < obstaclePrefabList.Length; i++)
        {

            for (int j = 0; j < obstaclePrefabList[i].obstacleList.Count; j++)
            {
                obstaclePrefabList[i].obstacleList[j].SetActive(false);
            }
        }
    }
}
