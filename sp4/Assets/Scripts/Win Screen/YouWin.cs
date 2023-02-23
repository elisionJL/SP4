using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class YouWin : MonoBehaviour
{
    public Image Background;
    private float AlphaChannel = 0, TimeDelay = 0f;

    // Update is called once per frame
    void Update()
    {
        TimeDelay += 1f * Time.deltaTime;

        if (TimeDelay >= 1f && AlphaChannel < 255)
        {
            AlphaChannel += 25 * Time.deltaTime;
            Background.color = new Color(Background.color.r, Background.color.g, Background.color.b, AlphaChannel / 255);
        }

        if (AlphaChannel >= 255)
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }
}
