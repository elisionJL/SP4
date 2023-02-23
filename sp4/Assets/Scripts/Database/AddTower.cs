using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.UI;

public class AddTower : MonoBehaviour
{
    public GameObject Player;
    public GameObject Inventory;
    public Sprite[] TowerImages = new Sprite[8];

    public GameObject[] Towers;
    public Material[] SkinMaterials;
    private int ChooseThisSkin;


    string URLReadTowers = GlobalStuffs.baseURL + "ReadTowers.php";
    string URLReadSkins = GlobalStuffs.baseURL + "ReadSkins.php";
    string URLReadTime = GlobalStuffs.baseURL + "ReadTime.php";
    string URLReadDate = GlobalStuffs.baseURL + "ReadDate.php";
    string URLReadPlayerStats = GlobalStuffs.baseURL + "ReadPlayerStatsJSON.php";

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        Inventory = GameObject.Find("_Inventory_");

        StartCoroutine(GetPlayerStats(GlobalStuffs.username));
        StartCoroutine(GetDateTime(GlobalStuffs.username));
        StartCoroutine(GetSkin(GlobalStuffs.username));
        StartCoroutine(GetTime(GlobalStuffs.username));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator GetTowers(string playername)
    {
        WWWForm form1 = new WWWForm();
        form1.AddField("username", playername);
        UnityWebRequest webRequest1 = UnityWebRequest.Post(URLReadTowers, form1);

        // Request and wait for the desired page.
        yield return webRequest1.SendWebRequest();

        switch (webRequest1.result)
        {
            case UnityWebRequest.Result.Success:
                Debug.Log("Yes");
                TowerStats TS = TowerStats.CreateFromJSON(webRequest1.downloadHandler.text);
                Debug.Log(webRequest1.downloadHandler.text);
                if (TS != null)
                {
                    GlobalStuffs.Tower[0] = TS.Tower1 - 1;
                    GlobalStuffs.Tower[1] = TS.Tower2 - 1;
                    GlobalStuffs.Tower[2] = TS.Tower3 - 1;
                    GlobalStuffs.Tower[3] = TS.Tower4 - 1;
                    GlobalStuffs.Tower[4] = TS.Tower5 - 1;

                    Debug.Log(TS.Tower1 + ", " + TS.Tower2 + ", " + TS.Tower3 + ", " + TS.Tower4 + ", " + TS.Tower5);
                }
                else
                {
                    GlobalStuffs.Tower[0] = 0;
                    GlobalStuffs.Tower[1] = 1;
                    GlobalStuffs.Tower[2] = 2;
                    GlobalStuffs.Tower[3] = 3;
                    GlobalStuffs.Tower[4] = 4;
                }

                for (int i = 0; i < 5; i++)
                {
                    Player.transform.GetChild(0).gameObject.GetComponent<Base_Interaction>().Towers.Add(Towers[GlobalStuffs.Tower[i]]);
                    Inventory.transform.GetChild(i).transform.GetChild(0).gameObject.GetComponent<Image>().sprite = TowerImages[GlobalStuffs.Tower[i]];
                }
                webRequest1.Dispose();
                break;
            default:
                Debug.Log("No");
                GlobalStuffs.Tower[0] = 0;
                GlobalStuffs.Tower[1] = 1;
                GlobalStuffs.Tower[2] = 2;
                GlobalStuffs.Tower[3] = 3;
                GlobalStuffs.Tower[4] = 4;

                for (int i = 0; i < 5; i++)
                {
                    Player.transform.GetChild(0).gameObject.GetComponent<Base_Interaction>().Towers.Add(Towers[GlobalStuffs.Tower[i]]);
                    Inventory.transform.GetChild(i).transform.GetChild(0).gameObject.GetComponent<Image>().sprite = TowerImages[GlobalStuffs.Tower[i]];
                }
                webRequest1.Dispose();
                break;
        }
    }
    IEnumerator GetSkin(string playername)
    {
        WWWForm form1 = new WWWForm();
        form1.AddField("username", playername);
        UnityWebRequest webRequest1 = UnityWebRequest.Post(URLReadSkins, form1);

        // Request and wait for the desired page.
        yield return webRequest1.SendWebRequest();

        switch (webRequest1.result)
        {
            case UnityWebRequest.Result.Success:
                Debug.Log("Skin Received: " + webRequest1.downloadHandler.text);
                if (webRequest1.downloadHandler.text == "R")
                {
                    ChooseThisSkin = 0;
                }
                else if (webRequest1.downloadHandler.text == "G")
                {
                    ChooseThisSkin = 1;
                }
                else
                {
                    ChooseThisSkin = 2;
                }
                Player.transform.GetChild(1).gameObject.transform.GetChild(4).gameObject.transform.GetComponent<SkinnedMeshRenderer>().material = SkinMaterials[ChooseThisSkin];
                webRequest1.Dispose();
                break;
            default:
                webRequest1.Dispose();
                break;
        }
    }
    IEnumerator GetTime(string playername)
    {
        WWWForm form1 = new WWWForm();
        form1.AddField("username", playername);
        UnityWebRequest webRequest1 = UnityWebRequest.Post(URLReadTime, form1);

        // Request and wait for the desired page.
        yield return webRequest1.SendWebRequest();

        switch (webRequest1.result)
        {
            case UnityWebRequest.Result.Success:
                Debug.Log("Time Received: " + webRequest1.downloadHandler.text);
                gameObject.GetComponent<UpdateDBAfterEveryWave>().timecountup = float.Parse(webRequest1.downloadHandler.text);
                webRequest1.Dispose();
                break;
            default:
                webRequest1.Dispose();
                break;
        }
    }
    IEnumerator GetDateTime(string playername)
    {
        WWWForm form1 = new WWWForm();
        form1.AddField("username", playername);
        UnityWebRequest webRequest1 = UnityWebRequest.Post(URLReadDate, form1);

        // Request and wait for the desired page.
        yield return webRequest1.SendWebRequest();

        switch (webRequest1.result)
        {
            case UnityWebRequest.Result.Success:
                Debug.Log("Date Received: " + webRequest1.downloadHandler.text);
                GlobalStuffs.LastLogin = webRequest1.downloadHandler.text;
                webRequest1.Dispose();
                break;
            default:
                webRequest1.Dispose();
                break;
        }
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
                    GlobalStuffs.username = "GuestPlayer";
                    GlobalStuffs.Hostages = 100;
                    GlobalStuffs.level = 0;
                    GlobalStuffs.TotalTimesPlayed = 0;
                }

                StartCoroutine(GetTowers(GlobalStuffs.username));
                webRequest.Dispose();
                break;
            default:
                GlobalStuffs.username = "GuestPlayer";
                GlobalStuffs.Hostages = 100;
                GlobalStuffs.level = 0;
                GlobalStuffs.TotalTimesPlayed = 0;
                webRequest.Dispose();
                break;
        }
    }
}
