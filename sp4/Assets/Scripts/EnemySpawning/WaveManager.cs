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
    public BGMManager bgmManager;
    public GameObject SpawnParticles;
    AudioSource countdownSFX;
    // Start is called before the first frame update
    void Start()
    {
        countdownSFX = GetComponent<AudioSource>();
        win = false;
        GameObject[] tempSpawnPoints;
        tempSpawnPoints = GameObject.FindGameObjectsWithTag("EnemySpawners");
        for(int i = 0; i <tempSpawnPoints.Length; ++i)
        {
            SpawnPoints.Add(tempSpawnPoints[i].GetComponent<EnemySpawner>());
        }
        waveText.text = "wave: " + wave.ToString() + "/" + maxWave.ToString();
        bgmManager.ChangeBGM(BGMManager.BGM.WAVEPREP);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(wave > maxWave)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            wave = maxWave;
            if (GlobalStuffs.level < maxWave - 2)
            {
                GlobalStuffs.level = maxWave - 2;
            }
            waveDone = true;
            win = true;
            waveCooldown = 10;
            StartCoroutine(UpdatePlayerStats());
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
                if (!countdownSFX.isPlaying && waveCooldown <= 5)
                {
                    countdownSFX.Play();
                }
            }
            else if (waveDone == true)
            {
                countdownSFX.Stop();
                TotalEnemies = 0;
                //generate the waves for the spawnpoints
                for (int i = 0; i < SpawnPoints.Count; ++i)
                {
                    TotalEnemies += SpawnPoints[i].GenerateWave(wave);
                }
                readyText.SetActive(false);
                waveDone = false;
                enemies.Clear();
                if (wave == maxWave)
                {
                    bgmManager.ChangeBGM(BGMManager.BGM.FINALBATTLE);
                }
                else
                {
                    bgmManager.ChangeBGM(BGMManager.BGM.WAVEBATTLE);
                }
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
                            Instantiate(SpawnParticles, Enemy.transform.position, Quaternion.identity);
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
                            GameObject.Find("Player").transform.GetChild(0).GetComponent<Player>().MinusSouls(-(250 * wave));
                            if(wave == maxWave)
                            {
                                bgmManager.ChangeBGM(BGMManager.BGM.FINALPREP);
                            }
                            else
                            {
                                bgmManager.ChangeBGM(BGMManager.BGM.WAVEPREP);
                            }
                            waveText.text = "wave: " + wave.ToString() + "/" + maxWave.ToString();
                        }
                        else
                        {
                            if(GlobalStuffs.level < maxWave - 2){
                                GlobalStuffs.level =  maxWave - 2;
                            }
                            waveDone = true;
                            win = true;
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
                Scene scene = SceneManager.GetActiveScene();
                if (scene.name == "FinalLevel")
                {
                    SceneManager.LoadScene("WinScene");
                }
                else {
                    SceneManager.LoadScene("LevelSelect");
                }
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
