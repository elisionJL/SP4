using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class TestTower : TowerBase
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        CanShoot = true;
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
        CanShoot = false;
        // tower_AI.GetQuaternionTarget(rootObject.transform,radius);
    }
    public override void OnUpdate()
    {
        if (tower_AI.GetQuaternionTarget(rootObject.transform, radius) == true && m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") && CanShoot)
        {
            Fire();
        }
        else if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName(NameOfAttack) && !CanShoot)
        {
            if (m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= FrameToReleaseAttack)
            {
                Instantiate(projectilePrefab, rootObject.transform.position, rootObject.transform.rotation);
                CanShoot = true;
            }
        }
    }
}
