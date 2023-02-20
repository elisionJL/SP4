using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class DemonGirlTower : TowerBase
{
    // Start is called before the first frame update
    void Start()
    {
        CanShoot = true;
        damage = 10;
        attackSpd = 1;
        cost = 300;
        hp = 10;
        tower_AI = GetComponent<Tower_AI>();
        radius = tower_AI.maxRadius;
        UpgradeCost = 150;
        tower_AI.HP = 100;
        Name = "Demon Girl";
    }

    public override void Fire()
    {
        m_Animator.SetTrigger("Attack");
        m_Animator.SetFloat("attackSpeed", attackSpd);
        CanShoot = false;
        //GameObject test = Instantiate(projectilePrefab, rootObject.transform.position, rootObject.transform.rotation);
        //test.GetComponent<projectile>().Set(damage, 10, radius * 1.2f);
    }
    public override void OnUpdate()
    {
        Transform target = tower_AI.GetQuaternionTarget(rootObject.transform, tower_AI.maxRadius);
        if (target != null && target.gameObject.GetComponent<Priest>() == null && target.gameObject.GetComponent<Heroine>() == null)
        {
            target.gameObject.GetComponent<Animator>().SetTrigger("Idle");
        }
        //Transform target = tower_AI.GetQuaternionTarget(rootObject.transform, tower_AI.maxRadius);
        if (tower_AI.GetQuaternionTarget(rootObject.transform, tower_AI.maxRadius) != null && m_Animator.GetCurrentAnimatorStateInfo(0).IsName("idle00") && CanShoot)
        {
            Fire();
        }
        else if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("attack01") && !CanShoot)
        {
            if (m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.36f)
            {
                if (target != null)
                {
                    target.gameObject.GetComponent<Enemy_AI>().MinusHP(damage);
                    target.gameObject.transform.position = gameObject.transform.position + gameObject.transform.forward * 2;
                }
                CanShoot = true;
            }
        }
    }
}
