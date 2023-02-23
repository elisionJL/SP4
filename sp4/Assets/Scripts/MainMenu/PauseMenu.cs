using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PauseMenu : MonoBehaviour
{
    public Slider VolumeSlider;
    public TMP_Text VolumePercentageText;
    public TMP_Text TimePlayedText;
    public TMP_Text DateText;
    public TMP_Text Username;
    public TMP_Text HighestLevelCleared;
    // Start is called before the first frame update
    void Start()
    {
        if (VolumeSlider == null)
        {   
            VolumeSlider = transform.GetChild(1).GetComponent<Slider>();
        }
        if (VolumePercentageText == null)
        {
            VolumePercentageText = transform.GetChild(2).GetComponent<TextMeshPro>();
        }
        VolumeSlider.value = AudioListener.volume;
        VolumePercentageText.text = Mathf.Ceil(VolumeSlider.value * 100).ToString() + "%";
    }

    // Update is called once per frame
    void Update()
    {
        if(AudioListener.volume != VolumeSlider.value)
        {
            AudioListener.volume = VolumeSlider.value;
            VolumePercentageText.text = Mathf.Ceil(VolumeSlider.value * 100).ToString() + "%";
        }

        if (gameObject.activeSelf)
        {
            int seconds, hours, minutes;
            seconds = (int)GameObject.Find("AddTowersToPlayer").GetComponent<UpdateDBAfterEveryWave>().timecountup;
            minutes = seconds / 60;
            hours = minutes / 60;
            TimePlayedText.text = "Total Time Played: " + ((int)hours).ToString() + "h " + ((int)(minutes % 60)).ToString() + "min " + ((int)(seconds % 60)) + "sec";

            DateText.text = "Last Login: " + GlobalStuffs.LastLogin;
            Username.text = "Username: " + GlobalStuffs.username;
            HighestLevelCleared.text = "Highest Level Cleared: " + GlobalStuffs.level.ToString();
        }
    }
}
