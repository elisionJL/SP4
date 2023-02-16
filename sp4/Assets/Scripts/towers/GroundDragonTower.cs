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
        tower_AI.maxRadius = 10;
        tower_AI.HP = 750;
        cost = 500;
        UpgradeCost = 250;
        CanShoot = true;
        Name = "GroundDragon";
    }

    public override void Fire()
    {
        m_Animator.SetTrigger("Attack");
        CanShoot = false;
    }
    public override void OnUpdate()
    {
        if (tower_AI.GetQuaternionTarget(rootObject.transform, tower_AI.maxRadius) != null && m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") && CanShoot)
        {
            Fire();
        }
        else if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") && !CanShoot)
        {
            if (m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.7f)
            {
                //hit the enemy here
                tower_AI.playerTransform.gameObject.GetComponent<Enemy_AI>().MinusHP(damage);
                CanShoot = true;
            }
        }
        //if (attackSpd > 0)
        //{
        //    attackSpd -= Time.deltaTime;
        //}
        ////get the target that the tower is aiming for
        //Transform target = tower_AI.GetQuaternionTarget(rootObject.transform, tower_AI.maxRadius);
        //if (target != null)
        //{
        //    if (attackSpd < 0)
        //    {
        //        Fire();
        //        attackSpd = 1;
        //    }
        //}
    }
}
