using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostageBribe : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha8))
            DebuffEnemies();
        else if (Input.GetKeyDown(KeyCode.Alpha9))
            StallEnemySpawn();
    }

    public void DebuffEnemies()
    {
        Enemy_AI[] Enemies = FindObjectsOfType<Enemy_AI>();

        for(int i = 0; i < Enemies.Length; i++)
        {
            Enemies[i].SetEnemyDebuffed();
        }
    }

    public void StallEnemySpawn()
    {
        EnemySpawner[] SpawnEnemies = FindObjectsOfType<EnemySpawner>();

        for(int i = 0; i < SpawnEnemies.Length; i++)
        {
            SpawnEnemies[i].SetStallSpawn();
        }
    }

    public void BuffTowers()
    {

    }
}
