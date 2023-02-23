using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetSelectedTowers : MonoBehaviour
{
    public Image[] Tower_Images = new Image[8];
    public TowerSelect TowerSelected;

    private bool success = false;

    // Start is called before the first frame update
    void Start()
    {
        TowerSelected = GameObject.Find("Canvas").GetComponent<TowerSelect>();
        StartCoroutine(GlobalStuffs.GetTowers(GlobalStuffs.username));
    }

    // Update is called once per frame
    void Update()
    {
        if(success == true)
        {
            for (int i = 0; i < 5; i++)
            {
                if (GlobalStuffs.Tower[i] != 0)
                {
                    transform.GetChild(i).gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = Tower_Images[GlobalStuffs.Tower[i] - 1].sprite;
                    TowerSelected.Monster_ID[i] = GlobalStuffs.Tower[i];
                }
            }

            success = false;
        }
    }

    public void SetSuccess()
    {
        success = true;
    }
}
