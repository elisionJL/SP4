using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

public class AddTower : MonoBehaviour
{
    public GameObject Player;
    public GameObject[] Towers;

    string URLReadTowers = GlobalStuffs.baseURL + "ReadTowers.php";

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        StartCoroutine(GetTowers(GlobalStuffs.username));
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
                    GlobalStuffs.Tower[0] = 1;
                    GlobalStuffs.Tower[1] = 2;
                    GlobalStuffs.Tower[2] = 3;
                    GlobalStuffs.Tower[3] = 4;
                    GlobalStuffs.Tower[4] = 5;
                }

                for (int i = 0; i < 5; i++)
                {
                    Player.transform.GetChild(0).gameObject.GetComponent<Base_Interaction>().Towers.Add(Towers[GlobalStuffs.Tower[i]]);
                }
                break;
            default:
                break;
        }
        webRequest1.Dispose();
    }
}
