using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class LogInRegScript : MonoBehaviour
{
    static string URLRegisterUser = GlobalStuffs.baseURL + "RegisterBackend.php";
    static string URLLoginUser = GlobalStuffs.baseURL + "LoginBackend.php";
    public TMP_InputField Password;
    public TMP_InputField ConfirmPassword;
    public TMP_InputField RegUsername;
    public TMP_InputField SignInUsername;
    public TMP_InputField SignInPassword;
    public TMP_Text ResponseSignup;
    public TMP_Text ResponseLogin;
    public string Username;
    public string UserPassword;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RegisterUser()
    {
        if (Password.text == ConfirmPassword.text)
        {
            Username = RegUsername.text;
            UserPassword = Password.text;
            StartCoroutine(Register());
        }
        else
        {
            ResponseSignup.text = "Passwords Do Not Match";
        }
    }
    public void LoginUser()
    {
        StartCoroutine(Login());
    }

    public IEnumerator Register()
    {
        WWWForm form = new WWWForm();
        form.AddField("sUsername", Username);
        form.AddField("sPassword", UserPassword);

        using (UnityWebRequest webreq = UnityWebRequest.Post(URLRegisterUser, form))
        {
            yield return webreq.SendWebRequest();
            switch (webreq.result)
            {
                case UnityWebRequest.Result.Success:
                    ResponseSignup.text = webreq.downloadHandler.text;
                    //this should auto create all the user in the tables you need with the correct username when they signup
                    webreq.Dispose();
                    break;
                default:
                    Debug.Log("baderror");
                    webreq.Dispose();
                    break;
            }
        }
    }
    public IEnumerator Login()
    {
        WWWForm form = new WWWForm();
        form.AddField("sUsername", SignInUsername.text);
        form.AddField("sPassword", SignInPassword.text);

        using (UnityWebRequest webreq = UnityWebRequest.Post(URLLoginUser, form))
        {
            yield return webreq.SendWebRequest();
            switch (webreq.result)
            {
                case UnityWebRequest.Result.Success:
                    ResponseLogin.text = webreq.downloadHandler.text;
                    if (webreq.downloadHandler.text == "Login Success")
                    {
                        GlobalStuffs.username = SignInUsername.text;
                        //Assign Your GlobalStuffs variable here
                        Debug.Log(GlobalStuffs.username);
                        SceneManager.LoadScene("PlayerSkinSelect");
                    }
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
