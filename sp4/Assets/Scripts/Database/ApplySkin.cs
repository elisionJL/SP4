using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

public class ApplySkin : MonoBehaviour
{
    public GameObject Player;
    public Material Text1;
    public Material Text2;
    public Material Text3;
    string URLReadSkins = GlobalStuffs.baseURL + "ReadSkins.php";
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetSkin(GlobalStuffs.username));
    }

    // Update is called once per frame
    void Update()
    {
        
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
                    Player.transform.GetComponent<SkinnedMeshRenderer>().material = Text1;
                    GetComponent<ChangeSkins>().CurrentSkin = "R";
                }
                else if (webRequest1.downloadHandler.text == "G")
                {
                    Player.transform.GetComponent<SkinnedMeshRenderer>().material = Text2;
                    GetComponent<ChangeSkins>().CurrentSkin = "G";
                }
                else
                {
                    Player.transform.GetComponent<SkinnedMeshRenderer>().material = Text3;
                    GetComponent<ChangeSkins>().CurrentSkin = "B";
                }
                webRequest1.Dispose();
                break;
            default:
                webRequest1.Dispose();
                break;
        }
    }
}
