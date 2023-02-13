using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class TestTower : TowerBase
{
    // Start is called before the first frame update
    void Start()
    {
        attackSpd = 1;
    }

    // Update is called once per frame
    void fixedUpdate()
    {
        
    }
   public override void Fire()
    {
        //Quaternion direction = Quaternion.LookRotation(transform.forward, transform.up);
        Instantiate(projectilePrefab, transform.position,Quaternion.Euler(transform.forward));
    }
    public override void OnUpdate()
    {
        if (attackSpd > 0)
        {
            attackSpd -= 0.1f;
        }
        else
        {
            Fire();
            attackSpd = 1;
        }
    }
}
