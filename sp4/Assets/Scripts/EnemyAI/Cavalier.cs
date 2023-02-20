using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Cavalier : MonoBehaviour
{
    public GameObject HitThisGuy;
    public Animator m_Animator;
    public bool CanShoot;
    public int Damage = 10;
    // Start is called before the first frame update
    void Start()
    {
        CanShoot = true;
    }

    public void Fire(GameObject Target, int Damage2)
    {
        if (CanShoot)
        {
            Damage = Damage2;
            HitThisGuy = Target;
            m_Animator.SetTrigger("Attack");
            CanShoot = false;
        }
        //GameObject test = Instantiate(projectilePrefab, rootObject.transform.position, rootObject.transform.rotation);
        //test.GetComponent<projectile>().Set(damage, 10, radius * 1.2f);
    }

    private float FindDistance (Transform Object1, Transform Object2)
    {
        if (Object2 == null)
        {
            return 10000;
        }
        else
        {
            return Vector3.Distance(Object1.position, Object2.position);
        }
    }
    public void Update()
    {
        //check if there is an enemy in the radius ,if there is ,trigger animation 
        if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") && !CanShoot)
        {
            if (m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.33f)
            {
                //hit the enemy here
                if (HitThisGuy != null)
                    HitThisGuy.GetComponent<Tower_AI>().MinusHP(Damage);

                CanShoot = true;
            }
        }
    }
}
