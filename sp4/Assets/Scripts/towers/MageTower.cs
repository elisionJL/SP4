using System.Collections;
using System.Collections.Generic;
using UnityEngine;
class MageTower : TowerBase
{
    // Start is called before the first frame update
    void Start()
    {
        CanShoot = true;

        damage = 10;

        attackSpd = 1;

        hp = 10;
        tower_AI = GetComponent<Tower_AI>();
        radius = tower_AI.maxRadius;
        tower_AI.HP = 100;

        Name = "Lich";
        cost = 400;
        UpgradeCost = 300;
    }

    public override void Fire()
    {
        m_Animator.SetTrigger("shoot");
        m_Animator.SetFloat("attackSpeed", attackSpd);
        CanShoot = false;
        // tower_AI.GetQuaternionTarget(rootObject.transform,radius);
    }
    public override void OnUpdate()
    {
        if (tower_AI.GetQuaternionTarget(rootObject.transform, radius) == true && m_Animator.GetCurrentAnimatorStateInfo(0).IsName("idle") && CanShoot)
        {
            Fire();
        }
        else if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("attack01") && !CanShoot)
        {
            if (m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.7f)
            {
                GameObject test = Instantiate(projectilePrefab, rootObject.transform.position, rootObject.transform.rotation);
                test.GetComponent<projectile>().Set(damage, 10, radius * 1.2f, 0, 1);
                CanShoot = true;
            }
        }
    }
}
