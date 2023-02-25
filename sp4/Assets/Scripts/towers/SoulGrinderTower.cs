using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class SoulGrinderTower : TowerBase
{
    public int soulsGenerated = 100;
    public GameObject[] ParticleSystems;

    // Start is called before the first frame update
    void Start()
    {
        damage = 0;
        Lvl = 1;
        attackSpd = 8;
        tower_AI = GetComponent<Tower_AI>();
        tower_AI.maxRadius = 0;
        tower_AI.HP = 10;
        tower_AI.HPSlider.maxValue = tower_AI.HP;
        tower_AI.HPSlider.value = tower_AI.HP;
        Name = "SoulGrinder";
        cost = 1000;
        UpgradeCost = 500;
    }
    public override void Fire()
    {
        GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).GetComponent<Player>().MinusSouls(-soulsGenerated);

        switch (Lvl)
        {
            case 1:
                ParticleSystems[0].GetComponent<ParticleSystem>().Play();
                break;
            case 2:
                ParticleSystems[1].GetComponent<ParticleSystem>().Play();
                break;
            case 3:
                ParticleSystems[2].GetComponent<ParticleSystem>().Play();
                break;
        }
    }

    public void SpawnParticles()
    {
        switch (Lvl)
        {
            case 1:
                ParticleSystems[0].gameObject.GetComponent<ParticleSystem>().Stop();
                break;
            case 2:
                ParticleSystems[1].gameObject.GetComponent<ParticleSystem>().Stop();
                break;
            case 3:
                ParticleSystems[2].gameObject.GetComponent<ParticleSystem>().Stop();
                break;
        }
    }

    public override void OnUpdate()
    {

        if (attackSpd > 0)
        {
            attackSpd -= Time.deltaTime;

            if(attackSpd <= 7.5f)
            {
                SpawnParticles();
            }
        }
        else
        {
            Fire();
            attackSpd = 8;
        }
    }

    public void UpgradeSouldGrinder()
    {
        Lvl += 1;
        soulsGenerated = Mathf.RoundToInt(soulsGenerated * 1.5f);
        attackSpd = 8;
    }

    public int GetSoulsGeneration()
    {
        return soulsGenerated;
    }

    public int GetSoulsUpgraded()
    {
        return Mathf.RoundToInt(soulsGenerated * 1.5f);
    }
}
