using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FindLevelCompleted : MonoBehaviour
{
    string URLReadPlayerStats = GlobalStuffs.baseURL + "ReadPlayerStatsJSON.php";
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetPlayerStats(GlobalStuffs.username));
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator GetPlayerStats(string playername)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", playername);
        UnityWebRequest webRequest = UnityWebRequest.Post(URLReadPlayerStats, form);

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
