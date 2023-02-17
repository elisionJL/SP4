using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class WaveManager : MonoBehaviour
{
    public int wave;
    public int maxWave;
    public float waveCooldown;
    public bool waveDone;
    public GameObject EnemyContainer;
    private int finishedSpawnPoints;
    public TextMeshProUGUI CountDownText;
    public List<EnemySpawner> SpawnPoints = new List<EnemySpawner>();
    public List<GameObject> enemies = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] tempSpawnPoints;
        tempSpawnPoints = GameObject.FindGameObjectsWithTag("EnemySpawners");
        for(int i = 0; i <tempSpawnPoints.Length; ++i)
        {
            SpawnPoints.Add(tempSpawnPoints[i].GetComponent<EnemySpawner>());
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(wave > maxWave)
        {
            CountDownText.enabled = false;
            return;
        }
        if (waveCooldown > 0 && waveDone == true)
        {
            waveCooldown -= Time.deltaTime;
            CountDownText.text = "Countdown: " + Mathf.Ceil(waveCooldown).ToString();
        }
        else if (waveDone == true)
        {
            //generate the waves for the spawnpoints
            for (int i = 0; i < SpawnPoints.Count; ++i)
            {
                SpawnPoints[i].GenerateWave(wave);
            }
            waveDone = false;
            enemies.Clear();
            CountDownText.enabled = false;
        }
        if (waveDone == false)
        {
            finishedSpawnPoints = 0;
            for (int i = 0; i < SpawnPoints.Count; ++i)
            {
                if (SpawnPoints[i].allSpawned == false)
                {
                    GameObject Enemy = SpawnPoints[i].SpawnEnemy();
                    if (Enemy != null)
                    {
                        Enemy.transform.SetParent(EnemyContainer.transform);
                    }
                }
                else
                {
                    ++finishedSpawnPoints;   
                }
            }
            if(finishedSpawnPoints == SpawnPoints.Count)
            {
                if(EnemyContainer.transform.childCount == 0)
                {
                    waveDone = true;
                    waveCooldown = 40;
                    ++wave;
                    CountDownText.enabled = true;
                }
            }
        }
    }
}
