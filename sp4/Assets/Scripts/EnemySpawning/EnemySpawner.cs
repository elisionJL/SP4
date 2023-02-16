using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<Enemy> enemyList = new List<Enemy>();
    public int wave;
    private float baseTime;
    public float valueScalar;
    public int maxWave;
    public int waveValue;
    public float TimeBetweenSpawn;
    public List<GameObject> EnemiesToSpawn = new List<GameObject>();
    int spawn;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void GenerateWave()
    {
        waveValue = (int)Mathf.Ceil(waveValue * valueScalar);
        List<GameObject> GeneratedEnemies = new List<GameObject>();
        List<Enemy> tempEnemyList = enemyList;
        int ValueToSpend = waveValue;
        int randomEnemy;
        Debug.Log("variables initialised");
        while (ValueToSpend > 0)
        {
            Debug.Log("randomize");
            //since its minInclusive to maxExclusive it will generate a number between 0 - (count-1)
            randomEnemy = Random.Range(0, tempEnemyList.Count);
            //in the future if cannot afford the enemy remove from temp list to reduce processing speed
            if (ValueToSpend - enemyList[randomEnemy].cost >= 0)
            {
                Debug.Log("Add Enemy");
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
        Debug.Log("Enemies Generated");
        EnemiesToSpawn.Clear();
        EnemiesToSpawn = GeneratedEnemies;
        baseTime = TimeBetweenSpawn;
        spawn = 0;
    }
    public void SpawnEnemy()
    {
        if (spawn < EnemiesToSpawn.Count)
        {
            if (TimeBetweenSpawn > 0)
            {
                TimeBetweenSpawn -= Time.deltaTime;
            }
            else
            {
                Instantiate(EnemiesToSpawn[spawn], transform.position, transform.rotation);
                ++spawn;
                TimeBetweenSpawn = baseTime;
            }
        }
    }
}

[System.Serializable]
public class Enemy
{
    public GameObject enemyPrefab;
    public int cost;

}