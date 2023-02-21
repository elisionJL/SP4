using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostageBribe : MonoBehaviour
{
    public GameObject Player;

    public void CloseUI()
    {
        gameObject.SetActive(false);
        Player.gameObject.GetComponent<Player>().LockMouse();
        Player.gameObject.GetComponent<Base_Interaction>().CloseShopUI();
        Player.gameObject.GetComponent<Base_Interaction>().SetUpgradeBool(false);
    }

    public void DebuffEnemies()
    {
        Enemy_AI[] Enemies = FindObjectsOfType<Enemy_AI>();

        for(int i = 0; i < Enemies.Length; i++)
        {
            Enemies[i].SetEnemyDebuffed();
        }

        CloseUI();
    }

    public void StallEnemySpawn()
    {
        EnemySpawner[] SpawnEnemies = FindObjectsOfType<EnemySpawner>();

        for(int i = 0; i < SpawnEnemies.Length; i++)
        {
            SpawnEnemies[i].SetStallSpawn();
        }

        CloseUI();
    }

    public void BuffTowers()
    {
        Tower_AI[] TowersFound = FindObjectsOfType<Tower_AI>();

        for(int i = 0; i < TowersFound.Length; i++)
        {
            TowersFound[i].BuffTowers();
        }

        CloseUI();
    }
}
