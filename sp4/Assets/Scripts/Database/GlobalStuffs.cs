using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using TMPro;

public static class GlobalStuffs {
    public static string username="GuestPlayer";
    public static int Hostages=100;
    public static int Souls=0;
    public static int level=0;
    public static int TotalTimesPlayed = 0;
    public static int[] Tower = new int[5];

    public static string baseURL= "http://localhost/Database/"; //rename this to your server path

    static string addscorebackendURL=baseURL+"addscorebackend.php";
    static string UpdatePlayerStatsURL = baseURL + ".php";
    static string GetTowersURL = baseURL + "ReadTowers.php";

    //public static IEnumerator DoSendScore(string pname,int score){
    //    WWWForm form=new WWWForm();
    //    form.AddField("sPlayerName",pname);
    //    form.AddField("iScore",score);
    //    UnityWebRequest webreq=UnityWebRequest.Post(addscorebackendURL,form);
    //    yield return webreq.SendWebRequest();
    //    switch (webreq.result)
    //        {
    //            case UnityWebRequest.Result.Success:
    //                Debug.Log(":\nReceived: " + webreq.downloadHandler.text);
    //                //GetScoreBoard();
    //                break;
    //            default:
    //                Debug.Log("error");
    //                break;
    //        }
    //        webreq.Dispose();            
    //}

    public static IEnumerator GetTowers(string TowersToGet)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);

        using (UnityWebRequest webreq = UnityWebRequest.Post(GetTowersURL, form))
        {
            yield return webreq.SendWebRequest();
            switch (webreq.result)
            {
                case UnityWebRequest.Result.Success:
                    Deserialize(webreq.downloadHandler.text); //added
                    break;
                default:
                    Debug.Log("error" + webreq.error);
                    break;
            }
            webreq.Dispose();
        }
    }
    //public static IEnumerator ClearScores(){
    //    UnityWebRequest webreq=UnityWebRequest.Get(DeleteAllScoreURL);
    //    yield return webreq.SendWebRequest();
    //    switch (webreq.result)
    //    {
    //            case UnityWebRequest.Result.Success:
    //                Debug.Log(":\nReceived: " + webreq.downloadHandler.text);
    //                //GetScoreBoard();
    //                break;
    //            default:
    //                Debug.Log("error");
    //                break;
    //    }
    //    webreq.Dispose();
    //}


    //public static IEnumerator DeleteUserStatsF()
    //{
    //    WWWForm form = new WWWForm();
    //    form.AddField("username", username);

    //    using (UnityWebRequest webreq = UnityWebRequest.Post(DeleteUserStatsURL, form))
    //    {
    //        yield return webreq.SendWebRequest();
    //        switch (webreq.result)
    //        {
    //            case UnityWebRequest.Result.Success:
    //                Debug.Log(":\nReceived: " + webreq.downloadHandler.text);
    //                //GetScoreBoard();
    //                break;
    //            default:
    //                Debug.Log("baderror");
    //                break;
    //        }
    //    }
    //    //webreq.Dispose();
    //}

    static string Deserialize(String RawJSON){ 
        ScoreList sb=JsonUtility.FromJson<ScoreList>(RawJSON); //convert raw json to objects
         
        string ddata= "Leaderboard:\n";               
        for(int a=0;a<sb.scores.Count;a++){
            OneScore oneScore=sb.scores[a];
            Debug.Log(oneScore.username+" : "+oneScore.score);
            ddata+=((a + 1) + "." + oneScore.username+" : "+oneScore.score+"\n");
        }

        return ddata;
    }

    public static IEnumerator UpdatePlayerStats(){
        WWWForm form=new WWWForm();
        form.AddField("username",username);

        using(UnityWebRequest webreq=UnityWebRequest.Post(UpdatePlayerStatsURL,form)){
        yield return webreq.SendWebRequest();
        switch (webreq.result)
            {
                case UnityWebRequest.Result.Success:
                    Debug.Log(":\nReceived: " + webreq.downloadHandler.text);
                    //GetScoreBoard();
                    break;
                default:
                    Debug.Log("baderror");
                    break;
            }
        }
        //webreq.Dispose();    
    }

    //    public static IEnumerator DeleteCurrentUser(){
    //    WWWForm form=new WWWForm();
    //    form.AddField("sUsername", username);        

    //    using(UnityWebRequest webreq=UnityWebRequest.Post(DeleteUserURL,form)){
    //    yield return webreq.SendWebRequest();
    //    switch (webreq.result)
    //        {
    //            case UnityWebRequest.Result.Success:
    //                Debug.Log(":\nReceived: " + webreq.downloadHandler.text);
    //                //GetScoreBoard();
    //                break;
    //            default:
    //                Debug.Log("baderror");
    //                break;
    //        }
    //    }
    //    //webreq.Dispose();

    //    }
}



//https://docs.unity3d.com/ScriptReference/Networking.UnityWebRequest.Get.html

//create classes and data structure to match the JSON structure
[Serializable]
class OneScore {
    public string username;
    public int score;    
}
[Serializable]
class ScoreList {
    public List<OneScore> scores=new List<OneScore>();
}

