using System.Collections;
using System.Collections.Generic;
using UnityEngine;
class DragonTower : TowerBase
{
    // Start is called before the first frame update
    void Start()
    {
        cost = 500;
        CanShoot = true;
        damage = 30;
        attackSpd = 1;
        hp = 10;
        tower_AI = GetComponent<Tower_AI>();
        tower_AI.maxRadius = 10;
        tower_AI.HP = 100;
        UpgradeCost = 250;
        Name = "Dragon";
    }

    public override void Fire()
    {
        m_Animator.SetTrigger("shoot");
        CanShoot = false;
        // tower_AI.GetQuaternionTarget(rootObject.transform,radius);
    }
    public override void OnUpdate()
    {
        Transform target = tower_AI.GetQuaternionTarget(rootObject.transform, tower_AI.maxRadius);
        if (tower_AI.GetQuaternionTarget(rootObject.transform, tower_AI.maxRadius) != null && m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") && CanShoot)
        {
            Fire();
        }
        else if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Fireball Shoot") && !CanShoot)
        {
            if (m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f)
            {
                GameObject test =Instantiate(projectilePrefab, rootObject.transform.position, rootObject.transform.rotation);
                test.GetComponent<projectile>().Set(damage, 10, tower_AI.maxRadius * 1.2f, 0);
                CanShoot = true;
            }
        }
    }

}
