using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
        if (cooldown > 0)
        {
            cooldown -= 1 * Time.deltaTime;
            cooldownText.text = "Cooldown: " + Mathf.RoundToInt(cooldown) + "s";
        }
        else
        {
            cooldownText.text = "";
        }
    }

    public void DebuffEnemies()
    {
        if(cooldown <= 0f)
        {
            Enemy_AI[] Enemies = FindObjectsOfType<Enemy_AI>();

            for (int i = 0; i < Enemies.Length; i++)
            {
                Enemies[i].SetEnemyDebuffed();
            }

            CloseUI();
            cooldown = 15f;
        }
    }

    public void StallEnemySpawn()
    {
        if(cooldown <= 0f)
        {
            EnemySpawner[] SpawnEnemies = FindObjectsOfType<EnemySpawner>();

            for (int i = 0; i < SpawnEnemies.Length; i++)
            {
                SpawnEnemies[i].SetStallSpawn();
            }

            CloseUI();
            cooldown = 15f;
        }
    }

    public void BuffTowers()
    {
        if(cooldown <= 0f)
        {
            Tower_AI[] TowersFound = FindObjectsOfType<Tower_AI>();

            for (int i = 0; i < TowersFound.Length; i++)
            {
                TowersFound[i].BuffTowers();
            }

            CloseUI();
            cooldown = 15f;
        }
    }
}
