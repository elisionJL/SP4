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
    protected int cost;
    protected int UpgradeCost;
    protected int sellValue;
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
        return hp;
    }

    public int GetHPUpgraded()
    {
        return Mathf.RoundToInt(hp * 1.5f);
    }
    public int GetDamage()
    {
        return damage;
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

    //public void NewUpgradeCost()
    //{
    //    UpgradeCost = (int)Mathf.Ceil(UpgradeCost * 1.5f);
    //}

    public void UpgradeStats()
    {
        hp = Mathf.RoundToInt(hp * 1.5f);
        damage = Mathf.RoundToInt(damage * 1.5f);
        Lvl += 1;
        UpgradeCost = Mathf.RoundToInt(UpgradeCost * 1.5f);
    }
}
