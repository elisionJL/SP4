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


    public TextMeshProUGUI displayTxt; //must add using UnityEngine.UI
    //public void OnButtonLogin()
    //{ //to be invoked by button click
    //    StartCoroutine(DoLogin());
    //}
    //IEnumerator DoLogin()
    //{
    //    WWWForm form = new WWWForm();
    //    form.AddField("sUsername", if_loginusername.text);
    //    form.AddField("sPassword", if_loginpassword.text);
    //    UnityWebRequest webreq = UnityWebRequest.Post(URLLoginBackend, form);
    //    yield return webreq.SendWebRequest();
    //    switch (webreq.result)
    //    {
    //        case UnityWebRequest.Result.Success:
    //            displayTxt.text = webreq.downloadHandler.text;
    //            if (webreq.downloadHandler.text == "Login Success")
    //            {
    //                Debug.Log("Load new Scene");
    //                GlobalStuffs.username = if_loginusername.text;
    //                GlobalStuffs.xp = 0;
    //                GlobalStuffs.cash = 0;
    //                StartCoroutine(GetPlayerStats(if_loginusername.text));
    //            }
    //            break;
    //        default:
    //            displayTxt.text = "server error";
    //            break;
    //    }
    //    webreq.Dispose();
    //}
    //public void OnButtonRegister()
    //{ //to be invoked by button click
    //    StartCoroutine(DoRegister());
    //}
    //IEnumerator DoRegister()
    //{
    //    WWWForm form = new WWWForm();
    //    form.AddField("sUsername", if_regusername.text);
    //    form.AddField("sPassword", if_regpassword.text);
    //    form.AddField("sEmail", if_regemail.text);
    //    UnityWebRequest webreq = UnityWebRequest.Post(URLRegBackend, form);
    //    yield return webreq.SendWebRequest();
    //    switch (webreq.result)
    //    {
    //        case UnityWebRequest.Result.Success:
    //            displayTxt.text = webreq.downloadHandler.text;
    //            break;
    //        default:
    //            displayTxt.text = "server error";
    //            break;
    //    }
    //    webreq.Dispose();
    //}
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
                    SceneManager.LoadScene("LevelOne");
                    break;
                default:
                    Debug.Log("baderror");
                    break;
            }
        }
        //webreq.Dispose();    
    }


    //public void GuestLogin()
    //{
    //    GlobalStuffs.username = "GuestPlayer";
    //    GlobalStuffs.Red = 87;
    //    GlobalStuffs.Green = 168;
    //    GlobalStuffs.Blue = 154;
    //    GlobalStuffs.xp = 0;
    //    GlobalStuffs.level = 1;
    //    GlobalStuffs.cash = 0;
    //    GlobalStuffs.TotalTimesPlayed = 0;
    //    GlobalStuffs.JumpUpgrade = 0;
    //    GlobalStuffs.TimeUpgrade = 0;
    //    GlobalStuffs.AllowCustomColour = 0;
    //    GlobalStuffs.GetCurrentMusic = 5;
    //    SceneManager.LoadScene("GameScn");
    //}

    //public void OnSendEmail()
    //{
    //    StartCoroutine(CheckEmailExist());
    //}

    //IEnumerator CheckEmailExist()
    //{
    //    WWWForm form = new WWWForm();
    //    form.AddField("Email", EmailInput.text);
    //    UnityWebRequest webreq = UnityWebRequest.Post(URLEmailCheckURL, form);
    //    yield return webreq.SendWebRequest();
    //    switch (webreq.result)
    //    {
    //        case UnityWebRequest.Result.Success:
    //            displayTxt.text = webreq.downloadHandler.text;
    //            break;
    //        default:
    //            displayTxt.text = "server error";
    //            break;
    //    }
    //    webreq.Dispose();
    //}

    //public void OnChangePassword()
    //{
    //    StartCoroutine(ChangePassword());
    //}

    //IEnumerator ChangePassword()
    //{
    //    WWWForm form = new WWWForm();
    //    form.AddField("Email", EmailInput.text);
    //    form.AddField("Password", PasswordInput.text);
    //    UnityWebRequest webreq = UnityWebRequest.Post(URLPasswordChange, form);
    //    yield return webreq.SendWebRequest();
    //    switch (webreq.result)
    //    {
    //        case UnityWebRequest.Result.Success:
    //            displayTxt.text = webreq.downloadHandler.text;
    //            break;
    //        default:
    //            displayTxt.text = "server error";
    //            break;
    //    }
    //    webreq.Dispose();
    //}
}

