using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class SkeletonTower : TowerBase
{
    // Start is called before the first frame update
    void Start()
    {
        damage = 10;
        attackSpd = 1;
        radius = 10;
        tower_AI = GetComponent<Tower_AI>();
        tower_AI.maxRadius = 7.5f;
        tower_AI.HP = 10;
        Name = "Skeleton";
        cost = 200;
        UpgradeCost = 100;
    }

    public override void Fire()
    {
        m_Animator.SetTrigger("Attack");
        CanShoot = false;
        //GameObject test = Instantiate(projectilePrefab, rootObject.transform.position, rootObject.transform.rotation);
        //test.GetComponent<projectile>().Set(damage, 10, radius * 1.2f);
    }
    public override void OnUpdate()
    {
        //Transform target = tower_AI.GetQuaternionTarget(rootObject.transform, tower_AI.maxRadius);
        if (tower_AI.GetQuaternionTarget(rootObject.transform, tower_AI.maxRadius) != null && m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Skeleton@Idle01") && CanShoot)
        {
            Debug.Log("Attacking");
            Fire();
        }
        else if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Skeleton@Attack01") && !CanShoot)
        {
            if (m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.3f)
            {
                //hit the enemy here
                tower_AI.MinusHP(10);
                CanShoot = true;
            }
        }
    }
}
