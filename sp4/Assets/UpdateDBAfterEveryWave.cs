using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class UpdateDBAfterEveryWave : MonoBehaviour
{
    static string URLSendTime = GlobalStuffs.baseURL + "UpdateTimePlayed.php";
    public float timecountup = GlobalStuffs.TotalTime;
    // Start is called before the first frame update
    void Start()
    {
        timecountup = GlobalStuffs.TotalTime;
    }

    // Update is called once per frame
    void Update()
    {
        timecountup += Time.unscaledDeltaTime;
    }

    public void UpdateTime()
    {
        Debug.Log("UPDATED TIME");
        GlobalStuffs.TotalTime = (int)timecountup;
        StartCoroutine(UpdateTimePlayed());
    }
    public static IEnumerator UpdateTimePlayed()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", GlobalStuffs.username);
        form.AddField("TimePlayed", GlobalStuffs.TotalTime);

        using (UnityWebRequest webreq = UnityWebRequest.Post(URLSendTime, form))
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
