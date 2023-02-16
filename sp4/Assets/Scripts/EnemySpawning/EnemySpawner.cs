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
    public GameObject arrow;
    private float Timerforarrow;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Timerforarrow += Time.deltaTime;
        if (Timerforarrow > 2)
        {
            Timerforarrow = 0;
            arrow.transform.position = this.transform.position;
            Instantiate(arrow);
        }
    }
    public void GenerateWave()
    {
        waveValue = (int)Mathf.Ceil(waveValue * valueScalar);
        List<GameObject> GeneratedEnemies = new List<GameObject>();
        List<Enemy> tempEnemyList = enemyList;
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
            if (TimeBetweenSpawn > 0)
            {
                TimeBetweenSpawn -= Time.deltaTime;
            }
            else
            {
                ++spawn;
                TimeBetweenSpawn = baseTime;
                return Instantiate(EnemiesToSpawn[spawn - 1], transform.position, transform.rotation);
            }
        }
        else
        {
            allSpawned = true;
        }
        return null;
    }
}

[System.Serializable]
public class Enemy
{
    public GameObject enemyPrefab;
    public int cost;
    public int waveRequired;
}