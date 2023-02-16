using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ArcherTower : TowerBase
{
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        cost = 200;
        CanShoot = true;
        damage = 10;
        attackSpd = 2;
        hp = 10;
        tower_AI = GetComponent<Tower_AI>();
        tower_AI.maxRadius = 20;
        tower_AI.HP = 100;

        Name = "Archer";
    }

    public override void Fire()
    {
        //m_Animator.SetTrigger("Attack");
        //CanShoot = false;
        // tower_AI.GetQuaternionTarget(rootObject.transform,radius);
        m_Animator.SetTrigger("Attack");
        GameObject test = Instantiate(projectilePrefab, rootObject.transform.position, rootObject.transform.rotation);
        test.GetComponent<projectile>().Set(damage, 10, tower_AI.maxRadius * 1.2f);
        attackSpd = 2;
        CanShoot = false;
        // tower_AI.GetQuaternionTarget(rootObject.transform,radius);
    }
    public override void OnUpdate()
    {
        target = tower_AI.GetQuaternionTarget(rootObject.transform, tower_AI.maxRadius);
        if (target != null && attackSpd < 0)
        {
            Fire();
        }
        else if (attackSpd > 0)
        {
            attackSpd -= Time.deltaTime;
        }
        //if (tower_AI.GetQuaternionTarget(rootObject.transform, tower_AI.maxRadius) != null && m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") && CanShoot)
        //{
        //    Debug.Log("GOBLIN SHOOT");
        //    Fire();
        //}
        //else if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") && !CanShoot)
        //{
        //    if (m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f)
        //    {
        //        GameObject test = Instantiate(projectilePrefab, rootObject.transform.position, rootObject.transform.rotation);
        //        test.GetComponent<projectile>().Set(damage, 10, tower_AI.maxRadius * 1.2f, 0);
        //        CanShoot = true;
        //    }
        //}
    }
}
