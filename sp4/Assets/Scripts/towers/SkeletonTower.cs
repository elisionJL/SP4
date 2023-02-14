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
    }

    public override void Fire()
    {
        m_Animator.SetTrigger("Attack");
        //GameObject test = Instantiate(projectilePrefab, rootObject.transform.position, rootObject.transform.rotation);
        //test.GetComponent<projectile>().Set(damage, 10, radius * 1.2f);
    }
    public override void OnUpdate()
    {
        Transform target = tower_AI.GetQuaternionTarget(rootObject.transform, tower_AI.maxRadius);
        if (target != null)
        {
            if (attackSpd > 0)
            {
                attackSpd -= Time.deltaTime;
            }
            else
            {
                Fire();
                attackSpd = 1;
            }
        }
    }
}
