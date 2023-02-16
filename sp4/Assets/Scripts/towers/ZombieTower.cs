using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ZombieTower : TowerBase
{
    // Start is called before the first frame update
    void Start()
    {
        damage = 1;
        attackSpd = 1;
        radius = 10;
        tower_AI = GetComponent<Tower_AI>();
        tower_AI.maxRadius = 10;
        tower_AI.HP = 1000;
        cost = 250;
        CanShoot = true;
        Name = "Zombie";
    }

    public override void Fire()
    {
        m_Animator.SetTrigger("Attack");
        CanShoot = false;
    }
    public override void OnUpdate()
    {
        if (tower_AI.GetQuaternionTarget(rootObject.transform, tower_AI.maxRadius) != null && m_Animator.GetCurrentAnimatorStateInfo(0).IsName("zombie_idle") && CanShoot)
        {
            Fire();
        }
        else if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("zombie_attack") && !CanShoot)
        {
            if (m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.3f)
            {
                //hit the enemy here
                tower_AI.playerTransform.gameObject.GetComponent<Enemy_AI>().MinusHP(damage);
                CanShoot = true;
            }
        }
    }
}
