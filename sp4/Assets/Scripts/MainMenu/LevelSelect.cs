using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
public class LevelSelect : MonoBehaviour
{
    public Transform Locks;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetPlayerStats());
        Cursor.lockState = CursorLockMode.Confined;
        for (int i = 0; i < GlobalStuffs.level + 1;++i)
        {
            Locks.GetChild(i).gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SelectLevel(string level)
    {
        SceneManager.LoadScene(level);
    }
    public void retrunToMainMenu()
    {
        SceneManager.LoadScene("TowerSelectScene");
    }
    IEnumerator GetPlayerStats()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", GlobalStuffs.username);
        UnityWebRequest webRequest = UnityWebRequest.Post(GlobalStuffs.URLReadPlayerStats, form);

        // Request and wait for the desired page.
        // Request and wait for the desired page.
        yield return webRequest.SendWebRequest();

        switch (webRequest.result)
        {
            case UnityWebRequest.Result.Success:
                PlayerStats PS = PlayerStats.CreateFromJSON(webRequest.downloadHandler.text);
                if (PS != null)
                {
                    GlobalStuffs.username = PS.username;
                    GlobalStuffs.Hostages = PS.hostages;
                    GlobalStuffs.level = PS.level;
                    GlobalStuffs.TotalTimesPlayed = PS.TotalTimesPlayed;
                }
                else
                {
                    GlobalStuffs.username = "Guest";
                    GlobalStuffs.Hostages = 100;
                    GlobalStuffs.level = 0;
                    GlobalStuffs.TotalTimesPlayed = 0;
                }

                webRequest.Dispose();
                break;
            default:
                GlobalStuffs.username = "Guest";
                GlobalStuffs.Hostages = 100;
                GlobalStuffs.level = 0;
                GlobalStuffs.TotalTimesPlayed = 0;
                webRequest.Dispose();
                break;
        }
    }
}
