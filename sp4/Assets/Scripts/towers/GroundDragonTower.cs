using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class GroundDragonTower : TowerBase
{
    // Start is called before the first frame update
    void Start()
    {
        damage = 25;
        attackSpd = 2;
        tower_AI = GetComponent<Tower_AI>();
        tower_AI.maxRadius = 5;
        tower_AI.HP = 750;
        cost = 500;

        Name = "GroundDragon";
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
            if (attackSpd < 0)
            {
                Fire();
                attackSpd = 1;
            }
        }
    }
}
