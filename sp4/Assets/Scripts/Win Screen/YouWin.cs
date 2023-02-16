using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class YouWin : MonoBehaviour
{
    public Image Background;
    private float AlphaChannel = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (AlphaChannel < 255)
        {
            AlphaChannel += 75 * Time.deltaTime;
            Background.color = new Color(Background.color.r, Background.color.g, Background.color.b, AlphaChannel / 255);
        }
    }
}