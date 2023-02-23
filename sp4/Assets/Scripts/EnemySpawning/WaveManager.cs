using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using TMPro;
public class WaveManager : MonoBehaviour
{
    public int wave;
    public int maxWave;
    public float waveCooldown;
    public bool waveDone;
    bool win;
    public GameObject EnemyContainer;
    private int finishedSpawnPoints;
    public TextMeshProUGUI CountDownText;
    public TextMeshProUGUI waveText;
    public GameObject readyText;
    public List<EnemySpawner> SpawnPoints = new List<EnemySpawner>();
    public List<GameObject> enemies = new List<GameObject>();
    public int TotalEnemies;

    // Start is called before the first frame update
    void Start()
    {
        win = false;
        GameObject[] tempSpawnPoints;
        tempSpawnPoints = GameObject.FindGameObjectsWithTag("EnemySpawners");
        for(int i = 0; i <tempSpawnPoints.Length; ++i)
        {
            SpawnPoints.Add(tempSpawnPoints[i].GetComponent<EnemySpawner>());
        }
        waveText.text = "wave: " + wave.ToString() + "/" + maxWave.ToString();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(wave > maxWave)
        {
            return;
        }
        if (!win)
        {
            if (Input.GetKeyDown(KeyCode.G) && waveCooldown > 0)
            {
                waveCooldown = -1;
                readyText.SetActive(false);
            }
            if (waveCooldown > 0 && waveDone == true)
            {
                waveCooldown -= Time.deltaTime;
                CountDownText.text = "Countdown: " + Mathf.Ceil(waveCooldown).ToString();

            }
            else if (waveDone == true)
            {
                TotalEnemies = 0;
                //generate the waves for the spawnpoints
                for (int i = 0; i < SpawnPoints.Count; ++i)
                {
                    TotalEnemies += SpawnPoints[i].GenerateWave(wave);
                }
                waveDone = false;
                enemies.Clear();
            }
            if (waveDone == false)
            {
                finishedSpawnPoints = 0;
                CountDownText.text = "Enemies: " + TotalEnemies;
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
                if (finishedSpawnPoints == SpawnPoints.Count)
                {
                    if (EnemyContainer.transform.childCount == 0)
                    {
                        if (wave != maxWave)
                        {
                            GameObject.Find("AddTowersToPlayer").GetComponent<UpdateDBAfterEveryWave>().UpdateTime();
                            waveDone = true;
                            readyText.SetActive(true);
                            waveCooldown = 40;
                            ++wave;
                            waveText.text = "wave: " + wave.ToString() + "/" + maxWave.ToString();
                        }
                        else
                        {
                            if(GlobalStuffs.level < maxWave - 2){
                                GlobalStuffs.level =  maxWave - 2;
                            }
                            waveDone = true;
                            waveCooldown = 10;
                            StartCoroutine(UpdatePlayerStats());
                        }
                    }
                }
            }
        }
        else
        {
            waveCooldown -= Time.deltaTime;
            CountDownText.text = "Level Complete!\nReturning in: " + Mathf.Ceil(waveCooldown).ToString();
            if (waveCooldown <= 0)
            {
                SceneManager.LoadScene("LevelSelect");
                
            }
        }
    }
    public static IEnumerator UpdatePlayerStats()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", GlobalStuffs.username);
        form.AddField("Hostage", GlobalStuffs.Hostages);
        form.AddField("LevelCleared", GlobalStuffs.level);
        form.AddField("TimesPlayed", GlobalStuffs.TotalTimesPlayed);

        using (UnityWebRequest webreq = UnityWebRequest.Post(GlobalStuffs.UpdatePlayerStatsURL, form))
        {
            yield return webreq.SendWebRequest();
            switch (webreq.result)
            {
                case UnityWebRequest.Result.Success:
                    Debug.Log(":\nReceived: " + webreq.downloadHandler.text);
                    webreq.Dispose();
                    break;
                default:
                    Debug.Log("baderror");
                    webreq.Dispose();
                    break;
            }
        }
    }
}
