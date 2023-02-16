using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class SoulGrinderTower : TowerBase
{
    public int soulsGenerated = 100;
    // Start is called before the first frame update
    void Start()
    {
        damage = 0;
        attackSpd = 5;
        tower_AI = GetComponent<Tower_AI>();
        tower_AI.maxRadius = 0;
        tower_AI.HP = 10;
        Name = "SoulGrinder";
        cost = 450;
        UpgradeCost = 225;
    }
    public override void Fire()
    {
       GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).GetComponent<Player>().Souls += soulsGenerated;
    }
    public override void OnUpdate()
    {

        if (attackSpd > 0)
        {
            attackSpd -= Time.deltaTime;
        }
        else
        {
            Fire();
            attackSpd = 5;
        }
    }
}
