using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InsultScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int InsultNumber = Random.Range(0, 25);

        switch(InsultNumber)
        {
            case 0:
                gameObject.GetComponent<TextMeshProUGUI>().text = "Gronk: Gronk maternal parent do better!";
                break;
            case 1:
                gameObject.GetComponent<TextMeshProUGUI>().text = "Gronk: Demon Lord die? Demon Lord not so stronk if Demon Lord died to 2 legged weaklings!";
                break;
            case 2:
                gameObject.GetComponent<TextMeshProUGUI>().text = "Gronk: It okay! Gronk take your place, then human will fear!";
                break;
            case 3:
                gameObject.GetComponent<TextMeshProUGUI>().text = "Gronk: Gronk don't know what Gronk means, but Gronk knows you died!";
                break;
            case 4:
                gameObject.GetComponent<TextMeshProUGUI>().text = "Gronk: Gronk can sell more than Demon lord can kill, Gronk thinks Demon Lord should become " +
                    "Demon bore";
                break;
            case 5:
                gameObject.GetComponent<TextMeshProUGUI>().text = "Developer A: Blame the guy who made bears fast.";
                break;
            case 6:
                gameObject.GetComponent<TextMeshProUGUI>().text = "Developer B: No, you suck, it says so above me after all";
                break;
            case 7:
                gameObject.GetComponent<TextMeshProUGUI>().text = "Developer C: Just upgrade your towers forehead!";
                break;
            case 8:
                gameObject.GetComponent<TextMeshProUGUI>().text = "Developer D: Environment keeps nuking itself!!!!!";
                break;
            case 9:
                gameObject.GetComponent<TextMeshProUGUI>().text = "Developer A: Useful tip, put down towers";
                break;
            case 10:
                gameObject.GetComponent<TextMeshProUGUI>().text = "Developer A: Another useful tip, stop the enemies before they reach the castle";
                break;
            case 11:
                gameObject.GetComponent<TextMeshProUGUI>().text = "Developer B: COFFEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE";
                break;
            case 12:
                gameObject.GetComponent<TextMeshProUGUI>().text = "Developer B: I would tell you to kill yourself, but you're already dead";
                break;
            case 13:
                gameObject.GetComponent<TextMeshProUGUI>().text = "Developer C: Placing towers is good civ - San Tzu, the art of war";
                break;
            case 14:
                gameObject.GetComponent<TextMeshProUGUI>().text = "Developer C: Famous Quote - I cant wait to go home and commit mass Genocide";
                break;
            case 15:
                gameObject.GetComponent<TextMeshProUGUI>().text = "Developer D: Github Sucks";
                break;
            case 16:
                gameObject.GetComponent<TextMeshProUGUI>().text = "Developer D: My laptop crashed and burned during the making of this game";
                break;
            case 17:
                gameObject.GetComponent<TextMeshProUGUI>().text = "Gronk: Demon Lord dead, does Demon Lord want Gronks feather? Gronk hears " +
                    "it can revive people.";
                break;
            case 18:
                gameObject.GetComponent<TextMeshProUGUI>().text = "Gronk: Gronk thank developers for making Gronk alive, but Gronk self aware," +
                    "Gronk no need developers to make Gronk speak";
                break;
            case 19:
                gameObject.GetComponent<TextMeshProUGUI>().text = "Gronk: Gronk knows Demon Lord dies because Player suck, play Demon Lord" +
                    " better!";
                break;
            case 20:
                gameObject.GetComponent<TextMeshProUGUI>().text = "Developer A: Gronk is becoming more aware! Someone stop him!";
                break;
            case 21:
                gameObject.GetComponent<TextMeshProUGUI>().text = "Gronk: Developers can't stop Gronk! Gronk just go to another game!!!";
                break;
            case 22:
                gameObject.GetComponent<TextMeshProUGUI>().text = "Developer B: Dev C has a bit of a mental issue";
                break;
            case 23:
                gameObject.GetComponent<TextMeshProUGUI>().text = "Developer B: No slander, only truth";
                break;
            case 24:
                gameObject.GetComponent<TextMeshProUGUI>().text = "Developer A: Yes";
                break;
            case 25:
                gameObject.GetComponent<TextMeshProUGUI>().text = "Gronk: Stop playing, you suck, touch grass";
                break;
        }
    }
}
