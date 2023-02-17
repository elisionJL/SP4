using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PauseMenu : MonoBehaviour
{
    public Slider VolumeSlider;
    public TMP_Text VolumePercentageText;
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
    }
}
