using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class SendPlayerSkins : MonoBehaviour
{

    static string URLSendSkins = GlobalStuffs.baseURL + "UpdatePlayerSkins.php";

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateSkins()
    {
        StartCoroutine(UpdateSelectedSkin());
    }
    public static IEnumerator UpdateSelectedSkin()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", GlobalStuffs.username);
        form.AddField("Skin", GlobalStuffs.PlayerSkins);

        using (UnityWebRequest webreq = UnityWebRequest.Post(URLSendSkins, form))
        {
            yield return webreq.SendWebRequest();
            switch (webreq.result)
            {
                case UnityWebRequest.Result.Success:
                    Debug.Log(":\nReceived: " + webreq.downloadHandler.text);
                    SceneManager.LoadScene("TowerSelectScene");
                    break;
                default:
                    Debug.Log("baderror");
                    break;
            }
        }
    }
}
