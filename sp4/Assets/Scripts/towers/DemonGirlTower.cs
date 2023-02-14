using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class DemonGirlTower : TowerBase
{
    // Start is called before the first frame update
    void Start()
    {
        damage = 10;
        attackSpd = 1;
        cost = 300;
        hp = 10;
        tower_AI = GetComponent<Tower_AI>();
        radius = tower_AI.maxRadius;
        UpgradeCost = 150;
    }

    public override void Fire()
    {
        m_Animator.SetTrigger("Attack");
        //GameObject test = Instantiate(projectilePrefab, rootObject.transform.position, rootObject.transform.rotation);
        //test.GetComponent<projectile>().Set(damage, 10, radius * 1.2f);
    }
    public override void OnUpdate()
    {
        if (tower_AI.GetQuaternionTarget(rootObject.transform, tower_AI.maxRadius) != null && m_Animator.GetCurrentAnimatorStateInfo(0).IsName("idle00") && CanShoot)
        {
            Debug.Log("fire1");
            Fire();
        }
        else if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("attack01") && !CanShoot)
        {
            if (m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.3f)
            {
                //hit the enemy here
                tower_AI.MinusHP(10);
                CanShoot = true;
            }
        }
    }
}
