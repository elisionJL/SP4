using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class TestTower : TowerBase
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        attackSpd = 1;
        tower_AI = GetComponent<Tower_AI>();
    }

    // Update is called once per frame
    void fixedUpdate()
    {
        
    }
   public override void Fire()
    {
        m_Animator.SetTrigger("shoot");
       // tower_AI.GetQuaternionTarget(rootObject.transform,radius);
       Instantiate(projectilePrefab, rootObject.transform.position, rootObject.transform.rotation);
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
