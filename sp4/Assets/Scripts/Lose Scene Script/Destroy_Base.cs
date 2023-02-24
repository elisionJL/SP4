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
    public GameObject MainMenuBtn;

    private float alphaColour_B, alphaColour_TXT;

    public GameObject[] Explosions;

    // Update is called once per frame
    void Update()
    {
        if(castleFinished == false) //If castle hasn't gone below the ground yet
            MoveCastle();
        else if(castleFinished == true) //Once castle has been destroyed
            DisplayUI();
    }

    public void MoveCastle()
    {
        //Move the castle downwards
        transform.position -= new Vector3(0, 3f * Time.deltaTime, 0);

        //Run a cooldown
        Cooldown += 1 * Time.deltaTime;

        //Once cooldown has finished, move castle left if bool is false then set it true
        if (BaseDirection == false && Cooldown >= 0.05f)
        {
            BaseDirection = true;
            Cooldown = 0;
            transform.position = new Vector3(-1, transform.position.y, transform.position.z);
        }

        //Once cooldown has finished, move castle right if bool is true then set it false
        else if (BaseDirection == true && Cooldown >= 0.05f)
        {
            BaseDirection = false;
            Cooldown = 0;
            transform.position = new Vector3(1, transform.position.y, transform.position.z);
        }

        //Once it has reached a certain y value, set castle finished to true
        if (transform.position.y < -5f)
            castleFinished = true;

        //If it has reached Checkpoint 1, 2, and 3, set explosion particle effect to active
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
        if(BackgroundDone == false) //If Background hasn't finished becoming opaque
        {
            if (alphaColour_B < 255) //If it hasn't become completely opaque
            {
                alphaColour_B += 75 * Time.deltaTime; //Slowly turn it opaque
                Background.color = new Color(Background.color.r, Background.color.g, Background.color.b, alphaColour_B / 255);
            }

            else if (alphaColour_B >= 255) //Once it is completely opaque
            {
                alphaColour_B = 255;
                Background.color = new Color(Background.color.r, Background.color.g, Background.color.b, alphaColour_B / 255);
                BackgroundDone = true; //Set the backgrounddone bool to true
                Background.GetComponent<AudioSource>().enabled = true;
            }
        }

        else if(BackgroundDone == true)
        {
            if (alphaColour_TXT < 255) //Start making the texts opaque
            {
                alphaColour_TXT += 75 * Time.deltaTime;
                YouSuck.color = new Color(YouSuck.color.r, YouSuck.color.g, YouSuck.color.b, alphaColour_TXT / 255);
                Insult.color = new Color(Insult.color.r, Insult.color.g, Insult.color.b, alphaColour_TXT / 255);
            }

            else if (alphaColour_TXT >= 255) //When the texts themselves have become opaque
            {
                Cursor.lockState = CursorLockMode.Confined;

                alphaColour_TXT = 255;
                YouSuck.color = new Color(YouSuck.color.r, YouSuck.color.g, YouSuck.color.b, alphaColour_TXT / 255);
                Insult.color = new Color(Insult.color.r, Insult.color.g, Insult.color.b, alphaColour_TXT / 255);
                MainMenuBtn.SetActive(true); //Show return to main menu button
            }
        }

    }
}
