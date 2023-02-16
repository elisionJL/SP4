using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class SkeletonTower : TowerBase
{
    // Start is called before the first frame update
    void Start()
    {
        CanShoot = true;
        damage = 10;
        attackSpd = 1;
        radius = 10;
        tower_AI = GetComponent<Tower_AI>();
        tower_AI.maxRadius = 7.5f;
        tower_AI.HP = 100;
        tower_AI.targetingMode = Tower_AI.TARGETING.CLOSEST;
        Name = "Skeleton";
        cost = 200;
        UpgradeCost = 100;
    }

    public override void Fire()
    {
        m_Animator.SetTrigger("Attack");
        CanShoot = false;
    }
    public override void OnUpdate()
    {
        Transform target = tower_AI.GetQuaternionTarget(rootObject.transform, tower_AI.maxRadius);
        //Transform target = tower_AI.GetQuaternionTarget(rootObject.transform, tower_AI.maxRadius);
        Debug.Log(target);
        if (target != null && m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Skeleton@Idle01") && CanShoot)
        {
            Debug.Log("Attacking");
            Fire();
        }
        else if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Skeleton@Attack01") && !CanShoot)
        {
            if (m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.3f)
            {
                if (target != null)
                {
                    target.gameObject.GetComponent<Enemy_AI>().MinusHP(damage);
                }
                CanShoot = true;
            }
        }
    }
}
