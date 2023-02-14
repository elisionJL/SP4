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
        tower_AI.maxRadius = 2;
        tower_AI.HP = 1000;
        cost = 250;
    }

    public override void Fire()
    {
        m_Animator.SetTrigger("Attack");
    }
    public override void OnUpdate()
    {
        if (attackSpd > 0)
        {
            attackSpd -= Time.deltaTime;
        }
        //get the target that the tower is aiming for
        Transform target = tower_AI.GetQuaternionTarget(rootObject.transform, tower_AI.maxRadius);
        if (target != null)
        {
            if(attackSpd < 0)
            {
                Fire();
                attackSpd = 1;
            }
        }
    }
}
