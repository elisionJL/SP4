using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Destroy_Base : MonoBehaviour
{
    bool BaseDirection = false;
    float Cooldown = 0f;
    bool castleFinished = false, BackgroundDone = false;

    public Image Background;
    public TextMeshProUGUI YouSuck;
    private float alphaColour_B, alphaColour_TXT;

    // Update is called once per frame
    void Update()
    {
        if(castleFinished == false)
            MoveCastle();
        else if(castleFinished == true)
            DisplayUI();
    }

    public void MoveCastle()
    {
        transform.position -= new Vector3(0, 3f * Time.deltaTime, 0);

        Cooldown += 1 * Time.deltaTime;

        if (BaseDirection == false && Cooldown >= 0.05f)
        {
            BaseDirection = true;
            Cooldown = 0;
            transform.position = new Vector3(-1, transform.position.y, transform.position.z);
        }

        else if (BaseDirection == true && Cooldown >= 0.05f)
        {
            BaseDirection = false;
            Cooldown = 0;
            transform.position = new Vector3(1, transform.position.y, transform.position.z);
        }

        if (transform.position.y < -5f)
            castleFinished = true;
    }

    public void DisplayUI()
    {
        if(BackgroundDone == false)
        {
            if (alphaColour_B < 255)
            {
                alphaColour_B += 75 * Time.deltaTime;
                Background.color = new Color(Background.color.r, Background.color.g, Background.color.b, alphaColour_B / 255);
            }

            else if (alphaColour_B >= 255)
            {
                alphaColour_B = 255;
                Background.color = new Color(Background.color.r, Background.color.g, Background.color.b, alphaColour_B / 255);
                BackgroundDone = true;
            }
        }

        else if(BackgroundDone == true)
        {
            if (alphaColour_TXT < 255)
            {
                alphaColour_TXT += 75 * Time.deltaTime;
                YouSuck.color = new Color(YouSuck.color.r, YouSuck.color.g, YouSuck.color.b, alphaColour_TXT / 255);
            }

            else if (alphaColour_TXT >= 255)
            {
                alphaColour_TXT = 255;
                YouSuck.color = new Color(YouSuck.color.r, YouSuck.color.g, YouSuck.color.b, alphaColour_TXT / 255);
            }
        }

    }
}
