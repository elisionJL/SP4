using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TowerSelect : MonoBehaviour
{
    public GameObject[] Selected_Towers;
    [SerializeField]
    public int[] Monster_ID;
    List<int> valuesToFind = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 };
    List<int> valuesFound = new List<int>();

    public void Start()
    {
        Monster_ID = new int[5];
    }

    public void Select_Towers(GameObject ButtonPressed) //Get the object reference of button pressed
    {
        for(int i = 0; i < Selected_Towers.Length; i++) //Run through the loop of the 5 selection ui for towers 
        {
            if(Selected_Towers[i].transform.GetChild(0).gameObject.GetComponent<Image>().sprite == null) //If a selection ui tower has no image set to it
            {
                bool CardExist = false; //base to be set as false whenever loop iterates

                for(int Check = 0; Check < i; Check++) //Do another for loop that checks if every other selection ui towers out of the 5 has the same image as the button that is press
                {
                    //If it finds one, then set bool to true since a card exists in the selection ui
                    if (Selected_Towers[Check].transform.GetChild(0).gameObject.GetComponent<Image>().sprite == ButtonPressed.transform.GetChild(0).gameObject.GetComponent<Image>().sprite)
                        CardExist = true;
                }

                //If it finishes the loop and no duplicate card exists
                if (CardExist == false)
                {
                    //Set the selection tower that has no image to the same image as the one that is attached to the button and set monster id to button monster id for database
                    Selected_Towers[i].transform.GetChild(0).gameObject.GetComponent<Image>().sprite = ButtonPressed.transform.GetChild(0).gameObject.GetComponent<Image>().sprite;
                    Monster_ID[i] = ButtonPressed.GetComponent<TowerCard>().Monster_ID;
                }
                break; //End the loop as we only want to change the first empty selection ui found
            }
        }
    }

    public void Deselect_Towers(GameObject ButtonPressed) //Get the object reference of button pressed
    {
        for (int i = 0; i < Selected_Towers.Length; i++) //Run through the loop of the 5 selection ui for towers 
        {
            if(Selected_Towers[i] == ButtonPressed) //If the selection ui is the same object as the one that is pressed
            {
                Selected_Towers[i].transform.GetChild(0).gameObject.GetComponent<Image>().sprite = null; //Set it's image to null
                Monster_ID[i] = 0; //And Monster ID to 0

                for (int NewObj = i; NewObj < Selected_Towers.Length; NewObj++) //Then run another loop starting from where the image pressed is found
                {
                    Debug.Log("NewObj: " + NewObj);
                    Debug.Log("Selected Towers: " + (Selected_Towers.Length - 1));

                    if((NewObj + 1) < Selected_Towers.Length) //If it has not reached the end of the list yet + 1
                    {
                        if (Selected_Towers[NewObj + 1] != null) //check if the last card is not null
                        {
                            //Replace current NewObj to the next one
                            Selected_Towers[NewObj].transform.GetChild(0).gameObject.GetComponent<Image>().sprite = Selected_Towers[NewObj + 1].transform.GetChild(0).gameObject.GetComponent<Image>().sprite;
                            Monster_ID[NewObj] = Monster_ID[NewObj + 1]; //Replace Monster ID as well with the next one
                        }

                        else if (Selected_Towers[NewObj + 1] == null) //Otherwise if the last one is null
                        {
                            Selected_Towers[NewObj].transform.GetChild(0).gameObject.GetComponent<Image>().sprite = null; //Make current one null too
                            Monster_ID[NewObj] = 0; //Same for Monster ID
                            break;
                        }
                    }

                    else if (NewObj == Selected_Towers.Length - 1) //Once it has reached the end of the list
                    {
                        Selected_Towers[NewObj].transform.GetChild(0).gameObject.GetComponent<Image>().sprite = null; //Set the last one to null
                        Monster_ID[NewObj] = 0; //And it's monster id to zero
                        break;
                    }
                }
            }
        }
    }

    public void GoNextScene()
    {
        foreach (int value in valuesToFind) //Find all values from array that player selected
        {
            if (System.Array.Exists(Monster_ID, element => element == value)) //If value to find is found
            {
                valuesFound.Add(value); //Add found to values found then repeat until end of Valuestofind List
            }
        }

        for (int i = 0; i < GlobalStuffs.Tower.Length; i++)
        {
            switch (i)
            {
                case 0:
                    if (Monster_ID[i] == 0) //If No towers are selected
                    {
                        if(valuesFound.Count == 0)
                        {
                            GlobalStuffs.Tower[0] = 1; //Set default to Dragon
                            valuesFound.Add(1); //Push to values found in list
                        }
                        else //Otherwise if even 1 tower is selected
                        {
                            for (int ValueToAdd = 1; ValueToAdd < 9; ValueToAdd++) //Use default towers to be put inside
                            {
                                bool ValueExists = false;

                                for (int CurrentNum = 0; CurrentNum < valuesFound.Count; CurrentNum++) //For loop to check if default tower value is already in the list
                                {
                                    if (ValueToAdd == valuesFound[CurrentNum]) //If ValuetoAdd is found in any part of valuesfound, end the loop and do another valuetoadd
                                        ValueExists = true;
                                }

                                if (ValueExists != true) //If we reach the end of the valuesfound list and value to add is not = to any value in the list
                                {
                                    GlobalStuffs.Tower[1] = ValueToAdd; //Set first value to valuetoadd
                                    valuesFound.Add(ValueToAdd); //Then push it to valuesfound for check of next line
                                    break;
                                }
                            }
                        }
                    }
                    else //If player has already chosen
                    {
                        GlobalStuffs.Tower[0] = Monster_ID[0];
                    }                
                    break;
                case 1:
                    if (Monster_ID[i] == 0) //If No towers are selected
                    {
                        for (int ValueToAdd = 1; ValueToAdd < 9; ValueToAdd++)
                        {
                            bool ValueExists = false;

                            for (int CurrentNum = 0; CurrentNum < valuesFound.Count; CurrentNum++)
                            {
                                if (ValueToAdd == valuesFound[CurrentNum]) //If ValuetoAdd is found in any part of valuesfound, end the loop and do another valuetoadd
                                    ValueExists = true;
                            }

                            if (ValueExists != true) //If we reach the end of the valuesfound list and value to add is not = to any value in the list
                            {
                                GlobalStuffs.Tower[1] = ValueToAdd; //Set first value to valuetoadd
                                valuesFound.Add(ValueToAdd); //Then push it to valuesfound for check of next line
                                break;
                            }
                        }
                    }
                    else
                    {
                        GlobalStuffs.Tower[1] = Monster_ID[1];
                    }
                    break;
                case 2:
                    if (Monster_ID[i] == 0) //If No towers are selected
                    {
                        for (int ValueToAdd = 1; ValueToAdd < 9; ValueToAdd++)
                        {
                            bool ValueExists = false;

                            for (int CurrentNum = 0; CurrentNum < valuesFound.Count; CurrentNum++)
                            {
                                if (ValueToAdd == valuesFound[CurrentNum]) //If ValuetoAdd is found in any part of valuesfound, end the loop and do another valuetoadd
                                    ValueExists = true;
                            }

                            if (ValueExists != true) //If we reach the end of the valuesfound list and value to add is not = to any value in the list
                            {
                                GlobalStuffs.Tower[2] = ValueToAdd; //Set first value to valuetoadd
                                valuesFound.Add(ValueToAdd); //Then push it to valuesfound for check of next line
                                break;
                            }
                        }
                    }
                    else
                    {
                        GlobalStuffs.Tower[2] = Monster_ID[2];
                    }
                    break;
                case 3:
                    if (Monster_ID[i] == 0) //If No towers are selected
                    {
                        for (int ValueToAdd = 1; ValueToAdd < 9; ValueToAdd++)
                        {
                            bool ValueExists = false;

                            for (int CurrentNum = 0; CurrentNum < valuesFound.Count; CurrentNum++)
                            {
                                if (ValueToAdd == valuesFound[CurrentNum]) //If ValuetoAdd is found in any part of valuesfound, end the loop and do another valuetoadd
                                    ValueExists = true;
                            }

                            if (ValueExists != true) //If we reach the end of the valuesfound list and value to add is not = to any value in the list
                            {
                                GlobalStuffs.Tower[3] = ValueToAdd; //Set first value to valuetoadd
                                valuesFound.Add(ValueToAdd); //Then push it to valuesfound for check of next line
                                break;
                            }
                        }
                    }
                    else
                    {
                        GlobalStuffs.Tower[3] = Monster_ID[3];
                    }
                    break;
                case 4:
                    if (Monster_ID[i] == 0) //If No towers are selected
                    {
                        for (int ValueToAdd = 1; ValueToAdd < 9; ValueToAdd++)
                        {
                            bool ValueExists = false;

                            for (int CurrentNum = 0; CurrentNum < valuesFound.Count; CurrentNum++)
                            {
                                if (ValueToAdd == valuesFound[CurrentNum]) //If ValuetoAdd is found in any part of valuesfound, end the loop and do another valuetoadd
                                    ValueExists = true;
                            }

                            if (ValueExists != true) //If we reach the end of the valuesfound list and value to add is not = to any value in the list
                            {
                                GlobalStuffs.Tower[4] = ValueToAdd; //Set first value to valuetoadd
                                valuesFound.Add(ValueToAdd); //Then push it to valuesfound for check of next line
                                break;
                            }
                        }
                    }
                    else
                    {
                        GlobalStuffs.Tower[4] = Monster_ID[4];
                    }
                    break;
            }
        }

        gameObject.GetComponent<TowerSet>().FinishTowerSelect(); //Move to pushing to database
    }
}
