using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class TowerBase : MonoBehaviour
{
    public enum PROJECTILE
    {

    }
    public PROJECTILE projectileType;
    public int damage;
    public int hp;
    public float attackSpd;
    public float radius;
    public int cost;
    public int sellValue;
    public Animator m_Animator;
    public Tower_AI tower_AI;
    //prefab for the projectile model
    public GameObject projectilePrefab;
    //reference for the center of the model will be used to 
    public GameObject rootObject;
    public bool CanShoot;
    public string NameOfAttack;
    public float FrameToReleaseAttack;
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
}
