using System.Collections;
using System.Collections.Generic;
using UnityEngine;
class DragonTower : TowerBase
{
    // Start is called before the first frame update
    void Start()
    {
        CanShoot = true;
        damage = 10;
        attackSpd = 1;
        hp = 10;
        tower_AI = GetComponent<Tower_AI>();
        tower_AI.maxRadius = 20;
    }

    public override void Fire()
    {
        m_Animator.SetTrigger("shoot");
        GameObject test =Instantiate(projectilePrefab, rootObject.transform.position, rootObject.transform.rotation);
        test.GetComponent<projectile>().Set(damage, 5, tower_AI.maxRadius * 1.2f);

        CanShoot = false;
        // tower_AI.GetQuaternionTarget(rootObject.transform,radius);
    }
    public override void OnUpdate()
    {
        Transform target = tower_AI.GetQuaternionTarget(rootObject.transform, tower_AI.maxRadius);
        if (tower_AI.GetQuaternionTarget(rootObject.transform, tower_AI.maxRadius) != null && m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") && CanShoot)
        {
            Debug.Log("fire1");
            Fire();
        }
        else if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Fireball Shoot") && !CanShoot)
        {
            if (m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f)
            {
                /*Debug.Log("fire2");
                GameObject test = Instantiate(projectilePrefab, rootObject.transform.position, rootObject.transform.rotation);
                test.GetComponent<projectile>().Set(damage, 10, radius * 1.2f);*/
                CanShoot = true;
            }
        }
    }
}
