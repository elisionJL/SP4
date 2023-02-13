using System.Collections;
using System.Collections.Generic;
using UnityEngine;
class TestTower : TowerBase
{
    // Start is called before the first frame update
    void Start()
    {
        damage = 10;
        attackSpd = 1;
        radius = 10;
        hp = 10;
        tower_AI = GetComponent<Tower_AI>();
    }

   public override void Fire()
    {
        m_Animator.SetTrigger("shoot");
        GameObject test =Instantiate(projectilePrefab, rootObject.transform.position, rootObject.transform.rotation);
        test.GetComponent<projectile>().Set(damage, 10, radius * 1.2f);

    }
    public override void OnUpdate()
    {
        if (attackSpd > 0)
        {
            attackSpd -= 0.1f;
        }
        else
        {
            if(tower_AI.GetQuaternionTarget(rootObject.transform, radius) == true)
            {
                Fire();
            }
           
            attackSpd = 1;
        }
    }
}
