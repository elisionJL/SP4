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

    }

    // Update is called once per frame
    void fixedUpdate()
    {
        
    }
   public override void Fire()
    {
       // Vector3 look = Quaternion.Lookat()
       // projectilePrefab.transform.position = transform.position;   
        Instantiate(projectilePrefab, transform.position, transform.rotation);
    }
    public override void OnUpdate()
    {
        Debug.Log(transform.forward);
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
