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
    protected float attackSpd;
    protected float radius;
    protected int cost;
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
        return name;
    }
    public int GetCost()
    {
        return cost;
    }

    public  int GetSellValue()
    {
        return sellValue;
    }
}
