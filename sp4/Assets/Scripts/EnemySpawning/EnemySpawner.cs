using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<Enemy> enemyList = new List<Enemy>();
    private float baseTime;
    public float valueScalar;
    public int waveValue;
    public float TimeBetweenSpawn;
    public List<GameObject> EnemiesToSpawn = new List<GameObject>();
    public bool allSpawned;
    int spawn;
    private GameObject EnemyContainer;
    public GameObject Waypoints;

    public bool stallSpawn = false;
    public float StallTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        #region ToBeTested
        if (StallTime >= 0)
        {
            StallTime -= 1 * Time.deltaTime;
        }
        else
        {
            stallSpawn = false;
            StallTime = 0f;
        }
        #endregion
    }
    public void GenerateWave(int waveNum)
    {
        waveValue = (int)Mathf.Ceil(waveValue * valueScalar);
        List<GameObject> GeneratedEnemies = new List<GameObject>();

        List<Enemy> tempEnemyList = new List<Enemy>();
        for(int i = 0; i < enemyList.Count; ++i)
        {
            if(enemyList[i].waveRequired <= waveNum)
            {
                tempEnemyList.Add(enemyList[i]);
            }
        }
        int ValueToSpend = waveValue;
        int randomEnemy;
        while (ValueToSpend > 0)
        {
            //since its minInclusive to maxExclusive it will generate a number between 0 - (count-1)
            randomEnemy = Random.Range(0, tempEnemyList.Count);
            //in the future if cannot afford the enemy remove from temp list to reduce processing speed
            if (ValueToSpend - enemyList[randomEnemy].cost >= 0)
            {
                GeneratedEnemies.Add(tempEnemyList[randomEnemy].enemyPrefab);
                ValueToSpend -= enemyList[randomEnemy].cost;
            }
            else
            {
                tempEnemyList.RemoveAt(randomEnemy);
                if(tempEnemyList.Count == 0)
                {
                    break;
                }
            }
        }
        allSpawned = false;
        EnemiesToSpawn.Clear();
        EnemiesToSpawn = GeneratedEnemies;
        baseTime = TimeBetweenSpawn;
        spawn = 0;
    } 
    public GameObject SpawnEnemy()
    {
        if (spawn < EnemiesToSpawn.Count)
        {
            if(stallSpawn == false)
            {
                if (TimeBetweenSpawn > 0)
                {
                    TimeBetweenSpawn -= Time.deltaTime;
                }
                else
                {
                    ++spawn;
                    TimeBetweenSpawn = baseTime;
                    EnemiesToSpawn[spawn - 1].gameObject.GetComponent<Enemy_AI>().GetWaypoints(Waypoints);
                    Debug.Log("Enemy No" + spawn); //Yuki Test
                    return Instantiate(EnemiesToSpawn[spawn - 1], transform.position, transform.rotation);
                }
            }
        }
        else
        {
            allSpawned = true;
        }
        return null;
    }
    #region ToBeTested
    public void setContainer(GameObject container)
    {
        EnemyContainer = container;
    }

    public void SetStallSpawn()
    {
        StallTime = 5f;
        stallSpawn = true;
    }
    #endregion
}

[System.Serializable]
public class Enemy
{
    public GameObject enemyPrefab;
    public int cost;
    public int waveRequired;
}