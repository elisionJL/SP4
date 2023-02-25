using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class TowerBase : MonoBehaviour
{
    public enum PROJECTILE
    {

    }
    public PROJECTILE projectileType;
    public string Name;
    protected int damage;
    protected int hp;
    protected int Lvl;
    protected float attackSpd;
    protected float radius;
    [SerializeField]
    protected int cost;
    protected int UpgradeCost;
    protected int sellValue;
    protected float BuffTime;

    public Animator m_Animator;
    public Tower_AI tower_AI;
    //prefab for the projectile model
    public GameObject projectilePrefab;
    //reference for the center of the model will be used to 
    public GameObject rootObject;
    public bool CanShoot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        OnUpdate();
    }
    public abstract void Fire();
    public abstract void OnUpdate();

    public string GetName()
    {
        return Name;
    }
    public int GetCost()
    {
        return cost;
    }

    public int GetHP()
    {
        return tower_AI.HP;
    }

    public int GetHPUpgraded()
    {
        return Mathf.RoundToInt(tower_AI.HP * 1.5f);
    }
    public int GetDamage()
    {
        return damage;
    }

    public float getAttackSpd()
    {
        return attackSpd;
    }

    public int GetDamageUpgraded()
    {
        return Mathf.RoundToInt(damage * 1.5f);
    }
    public  int GetSellValue()
    {
        return sellValue;
    }

    public int GetUpgradeCost()
    {
        return UpgradeCost;
    }

    public int GetLvl()
    {
        return Lvl;
    }

    public void RaiseLvl()
    {
        Lvl += 1;
    }

    public void UpgradeStats()
    {
        tower_AI.HP = Mathf.RoundToInt(tower_AI.HP * 1.5f);
        damage = Mathf.RoundToInt(damage * 1.25f);
        Lvl += 1;
        sellValue = Mathf.RoundToInt(sellValue * 1.3f);
        UpgradeCost = Mathf.RoundToInt(UpgradeCost * 1.5f);
    }

    public void Multipliers()
    {
        damage *= 2;
        attackSpd *= 1.3f;
        BuffTime = 5f;
    }

    public void BuffedTower()
    {
        if (gameObject.GetComponent<Tower_AI>().GetTowerBuffBool() == true)
        {
            BuffTime -= 1 * Time.deltaTime;

            if (BuffTime <= 0f)
            {
                damage /= 2;
                attackSpd /= 1.3f;
                gameObject.GetComponent<Tower_AI>().StopBuffs();
            }
        }
    }
}
