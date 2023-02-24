using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class HostageBribe : MonoBehaviour
{
    public GameObject Player;
    private float cooldown = 0f;
    public TextMeshProUGUI cooldownText;
    public void CloseUI()
    {
        gameObject.SetActive(false);
        Player.gameObject.GetComponent<Player>().LockMouse();
        Player.gameObject.GetComponent<Base_Interaction>().CloseShopUI();
        Player.gameObject.GetComponent<Base_Interaction>().SetUpgradeBool(false);
    }

    private void Update()
    {
        if (cooldown > 0) //If there is a cooldown time then minus it and show player there is a cooldown
        {
            cooldown -= 1 * Time.deltaTime;
            cooldownText.text = "Cooldown: " + Mathf.RoundToInt(cooldown) + "s";
        }
        else //Otherwise make it hidden
        {
            cooldownText.text = "";
        }
    }

    public void DebuffEnemies()
    {
        if(GlobalStuffs.Hostages >= 0) //If there are still hostages
        {
            if (cooldown <= 0f) //Then if cooldown has ended
            {
                Enemy_AI[] Enemies = FindObjectsOfType<Enemy_AI>(); //Find all enemies currently in the map

                for (int i = 0; i < Enemies.Length; i++) //Run a loop to go through all the enemies
                {
                    Enemies[i].SetEnemyDebuffed(); //Then debuff each enemy that is in the map
                }

                GlobalStuffs.Hostages -= 5; //Minus from hostages
                CheckIfNoHostages(); //Run function that checks if player has run out of hostages
                CloseUI(); //If there are still hostages then close the shop
                cooldown = 15f; //Set cooldown time
            }
        }
    }

    public void StallEnemySpawn()
    {
        if(GlobalStuffs.Hostages >= 0) //If there are still hostages
        {
            if (cooldown <= 0f) //Then if cooldown has ended
            {
                EnemySpawner[] SpawnEnemies = FindObjectsOfType<EnemySpawner>(); //Find all Spawners in the map

                for (int i = 0; i < SpawnEnemies.Length; i++)//Run a loop to go through all the Spawners
                {
                    SpawnEnemies[i].SetStallSpawn(); //Then stop the spawners from spawning enemies temporarily
                }

                GlobalStuffs.Hostages -= 10; //Minus from hostages
                CheckIfNoHostages(); //Run function that checks if player has run out of hostages
                CloseUI(); //If there are still hostages then close the shop
                cooldown = 15f; //Set cooldown time
            }
        }
    }

    public void BuffTowers()
    {
        if(GlobalStuffs.Hostages >= 0) //If there are still hostages
        {
            if (cooldown <= 0f)//Then if cooldown has ended
            {
                Tower_AI[] TowersFound = FindObjectsOfType<Tower_AI>(); ; //Find all Towers in the map

                for (int i = 0; i < TowersFound.Length; i++)//Run a loop to go through all the Towers
                {
                    TowersFound[i].BuffTowers(); //Buff all towers found
                }

                GlobalStuffs.Hostages -= 15; //Minus from hostages
                CheckIfNoHostages(); //Run function that checks if player has run out of hostages
                CloseUI(); //If there are still hostages then close the shop
                cooldown = 15f; //Set cooldown time
            }
        }
    }

    public void CheckIfNoHostages() //Function that checks if there are no more hostages
    {
        if (GlobalStuffs.Hostages <= 0) //If there are no more hostages or less
            SceneManager.LoadScene("LoseScene"); //Instantly send player to the lose scene
    }
}
