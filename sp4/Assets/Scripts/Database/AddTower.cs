using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

public class AddTower : MonoBehaviour
{
    public GameObject Player;
    public GameObject[] Towers;
    public Material[] SkinMaterials;
    private int ChooseThisSkin;

    string URLReadTowers = GlobalStuffs.baseURL + "ReadTowers.php";
    string URLReadSkins = GlobalStuffs.baseURL + "ReadSkins.php";
    string URLReadTime = GlobalStuffs.baseURL + "ReadTime.php";

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        StartCoroutine(GetTowers(GlobalStuffs.username));
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
                Debug.Log("Received: " + webRequest1.downloadHandler.text);

                TowerStats TS = TowerStats.CreateFromJSON(webRequest1.downloadHandler.text);
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
                }
                break;
            default:
                GlobalStuffs.Tower[0] = 0;
                GlobalStuffs.Tower[1] = 1;
                GlobalStuffs.Tower[2] = 2;
                GlobalStuffs.Tower[3] = 3;
                GlobalStuffs.Tower[4] = 4;

                for (int i = 0; i < 5; i++)
                {
                    Player.transform.GetChild(0).gameObject.GetComponent<Base_Interaction>().Towers.Add(Towers[GlobalStuffs.Tower[i]]);
                }
                break;
        }
        webRequest1.Dispose();
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
                Debug.Log("Received: " + webRequest1.downloadHandler.text);
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
                break;
            default:
                break;
        }
        webRequest1.Dispose();
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
                Debug.Log("Received: " + webRequest1.downloadHandler.text);
                gameObject.GetComponent<UpdateDBAfterEveryWave>().timecountup = float.Parse(webRequest1.downloadHandler.text);
                break;
            default:
                break;
        }
        webRequest1.Dispose();
    }
}
