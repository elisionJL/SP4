using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public bool TOF;
    public GameObject SettingsPanel;
    public TMP_InputField Text;
    public Slider BGM;

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
        SettingsPanel.SetActive(true);
    }
    public void ExitButton()
    {
        Application.Quit();
    }
    public void ClosePanel()
    {
        SettingsPanel.SetActive(false);
    }

    public void ChangeTextValue()
    {
        Text.text = "" + BGM.value;
    }
    public void ChangeSliderValue()
    {
        if (Text.text != "-" && Text.text != "")
        {
            if (int.Parse(Text.text) >= 0)
            {
                BGM.value = int.Parse(Text.text);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
