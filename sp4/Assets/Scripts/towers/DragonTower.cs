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

        damage = 10;

        attackSpd = 1;
        radius = 10;
        tower_AI = GetComponent<Tower_AI>();

        tower_AI.HP = 100;

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
       if (tower_AI.GetQuaternionTarget(rootObject.transform, radius) == true && m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") && CanShoot)
        {
            Fire();
        }
        else if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Fireball Shoot") && !CanShoot)
        {
            if (m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f)
            {
                GameObject test =Instantiate(projectilePrefab, rootObject.transform.position, rootObject.transform.rotation);
                test.GetComponent<projectile>().Set(damage, 10, radius * 1.2f);
                tower_AI.MinusHP(10);
                CanShoot = true;
            }
        }
    }

    public override string GetName()
    {
        return Name;
    }
    public override int GetCost()
    {
        return cost;
    }
    public override int GetSellValue()
    {
        return sellValue;
    }
}
