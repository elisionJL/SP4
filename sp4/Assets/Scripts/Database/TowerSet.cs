using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class TowerSet : MonoBehaviour
{
    string URLReadTowers = GlobalStuffs.baseURL + "ReadTowers.php";
    static string UpdateTowersURL = GlobalStuffs.baseURL + "UpdateTowers.php";

    public void FinishTowerSelect()
    {
        StartCoroutine(UpdateTowers());
    }

    public static IEnumerator UpdateTowers()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", GlobalStuffs.username);
        form.AddField("Tower1", GlobalStuffs.Tower[0]);
        form.AddField("Tower2", GlobalStuffs.Tower[1]);
        form.AddField("Tower3", GlobalStuffs.Tower[2]);
        form.AddField("Tower4", GlobalStuffs.Tower[3]);
        form.AddField("Tower5", GlobalStuffs.Tower[4]);

        using (UnityWebRequest webreq = UnityWebRequest.Post(UpdateTowersURL, form))
        {
            yield return webreq.SendWebRequest();
            switch (webreq.result)
            {
                case UnityWebRequest.Result.Success:
                    Debug.Log(":\nReceived: " + webreq.downloadHandler.text);
                    SceneManager.LoadScene("LevelSelect");
                    break;
                default:
                    Debug.Log("baderror");
                    SceneManager.LoadScene("LevelSelect");
                    break;
            }
        }
        //webreq.Dispose();    
    }
}

