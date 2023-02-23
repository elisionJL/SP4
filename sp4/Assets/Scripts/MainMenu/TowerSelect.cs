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

    public void Select_Towers(GameObject ButtonPressed)
    {
        for(int i = 0; i < Selected_Towers.Length; i++)
        {
            if(Selected_Towers[i].transform.GetChild(0).gameObject.GetComponent<Image>().sprite == null)
            {
                bool CardExist = false;

                for(int Check = 0; Check < i; Check++)
                {
                    if (Selected_Towers[Check].transform.GetChild(0).gameObject.GetComponent<Image>().sprite == ButtonPressed.transform.GetChild(0).gameObject.GetComponent<Image>().sprite)
                        CardExist = true;
                }

                if (CardExist == false)
                {
                    Selected_Towers[i].transform.GetChild(0).gameObject.GetComponent<Image>().sprite = ButtonPressed.transform.GetChild(0).gameObject.GetComponent<Image>().sprite;
                    Monster_ID[i] = ButtonPressed.GetComponent<TowerCard>().Monster_ID;
                }
                break;
            }
        }
    }

    public void Deselect_Towers(GameObject ButtonPressed)
    {
        for (int i = 0; i < Selected_Towers.Length; i++)
        {
            if(Selected_Towers[i] == ButtonPressed)
            {
                Selected_Towers[i].transform.GetChild(0).gameObject.GetComponent<Image>().sprite = null;
                Monster_ID[i] = 0;

                for (int NewObj = i; NewObj < Selected_Towers.Length; NewObj++)
                {
                    Debug.Log("NewObj: " + NewObj);
                    Debug.Log("Selected Towers: " + (Selected_Towers.Length - 1));

                    if((NewObj + 1) < Selected_Towers.Length)
                    {
                        if (Selected_Towers[NewObj + 1] != null)
                        {
                            Selected_Towers[NewObj].transform.GetChild(0).gameObject.GetComponent<Image>().sprite = Selected_Towers[NewObj + 1].transform.GetChild(0).gameObject.GetComponent<Image>().sprite;
                            Monster_ID[NewObj] = Monster_ID[NewObj + 1];
                        }

                        else if (Selected_Towers[NewObj + 1] == null)
                        {
                            Selected_Towers[NewObj].transform.GetChild(0).gameObject.GetComponent<Image>().sprite = null;
                            Monster_ID[NewObj] = 0;
                            break;
                        }
                    }

                    else if (NewObj == Selected_Towers.Length - 1)
                    {
                        Selected_Towers[NewObj].transform.GetChild(0).gameObject.GetComponent<Image>().sprite = null;
                        Monster_ID[NewObj] = 0;
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
            if (System.Array.Exists(Monster_ID, element => element == value))
            {
                valuesFound.Add(value);
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
                            GlobalStuffs.Tower[0] = 1;
                            valuesFound.Add(1);
                        }
                        else
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
                    }
                    else
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

        gameObject.GetComponent<TowerSet>().FinishTowerSelect();
    }
}
