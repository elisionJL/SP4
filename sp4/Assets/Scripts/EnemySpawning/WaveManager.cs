using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public float waveCooldown;
    public bool waveDone;
    public List<EnemySpawner> SpawnPoints = new List<EnemySpawner>();
    // Start is called before the first frame update
    void Start()
    {
        //SpawnPoints[0].GenerateWave();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (waveCooldown > 0 && waveDone == true)
        {
            waveCooldown -= Time.deltaTime;
        }
        else if (waveDone == true)
        {
            //generate the waves for the spawnpoints
            for (int i = 0; i < SpawnPoints.Count; ++i)
            {
                SpawnPoints[i].GenerateWave();
            }
            waveDone = false;
        }
        if (waveDone == false)
        {
            for (int i = 0; i < SpawnPoints.Count; ++i)
            {
                SpawnPoints[i].SpawnEnemy();
            }
        }
    }
}
