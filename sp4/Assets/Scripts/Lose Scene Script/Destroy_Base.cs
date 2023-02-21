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
    public TextMeshProUGUI Insult;
    private float alphaColour_B, alphaColour_TXT;

    public GameObject[] Explosions;

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

        if (gameObject.transform.position.y < 5.92f && Explosions[0].activeSelf == false)
        {
            Explosions[0].SetActive(true);
            Explosions[0].GetComponent<AudioSource>().enabled = true;
        }
        if (gameObject.transform.position.y < 3.88f && Explosions[1].activeSelf == false)
        {
            Explosions[1].SetActive(true);
            Explosions[1].GetComponent<AudioSource>().enabled = true;
        }
        if (gameObject.transform.position.y < 1.16f && Explosions[2].activeSelf == false)
        {
            Explosions[2].SetActive(true);
            Explosions[2].GetComponent<AudioSource>().enabled = true;
        }
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
                Background.GetComponent<AudioSource>().enabled = true;
            }
        }

        else if(BackgroundDone == true)
        {
            if (alphaColour_TXT < 255)
            {
                alphaColour_TXT += 75 * Time.deltaTime;
                YouSuck.color = new Color(YouSuck.color.r, YouSuck.color.g, YouSuck.color.b, alphaColour_TXT / 255);
                Insult.color = new Color(Insult.color.r, Insult.color.g, Insult.color.b, alphaColour_TXT / 255);
            }

            else if (alphaColour_TXT >= 255)
            {
                alphaColour_TXT = 255;
                YouSuck.color = new Color(YouSuck.color.r, YouSuck.color.g, YouSuck.color.b, alphaColour_TXT / 255);
                Insult.color = new Color(Insult.color.r, Insult.color.g, Insult.color.b, alphaColour_TXT / 255);
            }
        }

    }
}
