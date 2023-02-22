using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.Networking;

public class MainMenu : MonoBehaviour
{
    public bool TOF;
    public GameObject SettingsPanel;
    public Slider MasterVolumeSlider;
    public TMP_InputField MasterVolumeText;
    public Slider BGMVolumeSlider;
    public TMP_InputField BGMVolumeText;
    public Slider SFXVolumeSlider;
    public TMP_InputField SFXVolumeText;
    public AudioMixer audioMixer;
    // Start is called before the first frame update
    void Start()
    {
        if (TOF)
            SettingsPanel.SetActive(false);
    }

    public void StartButton()
    {
        SceneManager.LoadScene("TowerSelectScene");
    }
    public void SettingsButton()
    {
        StartCoroutine("GetPlayerSettings");
    }
    public void ExitButton()
    {
        Application.Quit();
        
    }
    public void ClosePanel()
    {
        StartCoroutine("UpdatePlayerSettings");
    }
    public  IEnumerator UpdatePlayerSettings()
    {
        WWWForm form = new WWWForm();
        form.AddField("username",  GlobalStuffs.username);
        form.AddField("newbgm", BGMVolumeSlider.value.ToString());
        form.AddField("newsfx", SFXVolumeSlider.value.ToString());
        form.AddField("newmaster", MasterVolumeSlider.value.ToString());
        using (UnityWebRequest webreq = UnityWebRequest.Post(GlobalStuffs.UpdateSettingsURL, form))
        {
            yield return webreq.SendWebRequest();
            switch (webreq.result)
            {
                case UnityWebRequest.Result.Success:
                    SettingsPanel.SetActive(false);
                    webreq.Dispose();
                    break;
                default:
                    webreq.Dispose();
                    Debug.Log("baderror");
                    webreq.Dispose();
                    break;
            }
        } 
    }

    public  IEnumerator GetPlayerSettings()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", GlobalStuffs.username);
        UnityWebRequest webRequest = UnityWebRequest.Post(GlobalStuffs.ReadSettingsURL, form);
        // Request and wait for the desired page.
        yield return webRequest.SendWebRequest();
        switch (webRequest.result)
        {
            case UnityWebRequest.Result.Success:
                PlayerSettings ps = null;
                if (webRequest.downloadHandler.text != " ")
                    ps = PlayerSettings.CreateFromJSON(webRequest.downloadHandler.text);
                if (ps != null)
                {
                    GlobalStuffs.username = ps.username;
                    GlobalStuffs.sfxVolume = ps.sfxVolume;
                    GlobalStuffs.bgmVolume = ps.bgmVolume;
                    GlobalStuffs.masterVolume = ps.masterVolume;
                    SettingsPanel.SetActive(true);
                }
                break;
            default:
                break;
        }
        webRequest.Dispose();

    }
    public void ChangeBGMSliderValue()
    {
        int value = (int)Mathf.Ceil(BGMVolumeSlider.value);
        if (value > 100)
        {
            BGMVolumeText.text = "100%";
        }
        else if (value < 0)
        {
            BGMVolumeText.text = "0%";
        }
        else
        {
            BGMVolumeText.text = value + "%";
        }
        audioMixer.SetFloat("BGMVolume", BGMVolumeSlider.value - 80);
    }

    public void ChangeSFXSliderValue()
    {
        int value = (int)Mathf.Ceil(SFXVolumeSlider.value);
        if (value > 100)
        {
            SFXVolumeText.text = "100%";
        }
        else if (value < 0)
        {
            SFXVolumeText.text = "0%";
        }
        else
        {
            SFXVolumeText.text = value + "%";
        }
        audioMixer.SetFloat("SFXVolume", SFXVolumeSlider.value - 80);
    }
    public void ChangeMasterSliderVolume()
    {
        int value = (int)Mathf.Ceil(MasterVolumeSlider.value);
        if (value > 100)
        {
            MasterVolumeText.text = "100%";
        }
        else if (value < 0)
        {
            MasterVolumeText.text = "0%";
        }
        else
        {
            MasterVolumeText.text = value + "%";
        }
        audioMixer.SetFloat("MasterVolume", MasterVolumeSlider.value - 80);
    }

    public void ChangeBGMText()
    {
        int value;
        if (int.TryParse(BGMVolumeText.text, out value))
        {
            if (value > 100)
            {
                BGMVolumeText.text = "100%";
                BGMVolumeSlider.value = 100;
            }
            else if (value < 0)
            {
                BGMVolumeText.text = "0%";
                BGMVolumeSlider.value = 0;
            }
            else
            {
                BGMVolumeText.text = value + "%";
                BGMVolumeSlider.value = value;
            }
        }
        audioMixer.SetFloat("BGMVolume", BGMVolumeSlider.value - 80);
    }

    public void ChangeSFXText()
    {
        int value;
        if (int.TryParse(SFXVolumeText.text, out value))
        {
            if (value > 100)
            {
                SFXVolumeText.text = "100%";
                SFXVolumeSlider.value = 100;
            }
            else if (value < 0)
            {
                SFXVolumeText.text = "0%";
                SFXVolumeSlider.value = 0;
            }
            else
            {
                SFXVolumeText.text = value + "%";
                SFXVolumeSlider.value = value;
            }
        }
        audioMixer.SetFloat("SFXVolume", SFXVolumeSlider.value - 80);
    }

    public void ChangeMasterText()
    {
        int value;
        if (int.TryParse(MasterVolumeText.text, out value))
        {
            if (value > 100)
            {
                MasterVolumeText.text = "100%";
                MasterVolumeSlider.value = 100;
            }
            else if (value < 0)
            {
                MasterVolumeText.text = "0%";
                MasterVolumeSlider.value = 0;
            }
            else
            {
                MasterVolumeText.text = value + "%";
                MasterVolumeSlider.value = value;
            }
        }
        audioMixer.SetFloat("MasterVolume", MasterVolumeSlider.value - 80);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
