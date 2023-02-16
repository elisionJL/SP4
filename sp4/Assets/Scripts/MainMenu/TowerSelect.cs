using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TowerSelect : MonoBehaviour
{
    public GameObject[] Selected_Towers;
    [SerializeField]
    private int[] Monster_ID;

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

                if(i > 0 && i < 4)
                {
                    for(int NewObj = i; NewObj < Selected_Towers.Length; NewObj++)
                    {
                        if(Selected_Towers[NewObj + 1] != null)
                        {
                            Selected_Towers[NewObj] = Selected_Towers[NewObj + 1]; 
                        }
                    }
                }
            }
        }
    }

    public void GoNextScene()
    {
        SceneManager.LoadScene("WhateverNextSceneIs"); //To be changed when we have decided on a scene
    }
}
