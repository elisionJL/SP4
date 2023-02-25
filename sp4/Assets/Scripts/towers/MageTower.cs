using System.Collections;
using System.Collections.Generic;
using UnityEngine;
class MageTower : TowerBase
{
    AudioSource m_AudioSource;
    // Start is called before the first frame update
    void Start()
    {
        CanShoot = true;
        m_AudioSource = GetComponent<AudioSource>();
        damage = 10;

        attackSpd = 1;
        Lvl = 1;
        tower_AI = GetComponent<Tower_AI>();
        radius = tower_AI.maxRadius;
        tower_AI.HP = 100;
        tower_AI.HPSlider.maxValue = tower_AI.HP;

        Name = "Lich";
        cost = 250;
        UpgradeCost = 125;

    }

    public override void Fire()
    {
        m_Animator.SetTrigger("shoot");
        m_Animator.SetFloat("attackSpeed", attackSpd);
        CanShoot = false;
        // tower_AI.GetQuaternionTarget(rootObject.transform,radius);
    }
    public override void OnUpdate()
    {
        if (tower_AI.GetQuaternionTarget(rootObject.transform, radius) == true && m_Animator.GetCurrentAnimatorStateInfo(0).IsName("idle") && CanShoot)
        {
            Fire();
        }
        else if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("attack01") && !CanShoot)
        {
            
            if (m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.7f)
            {
                if (!m_AudioSource.isPlaying)
                {
                    m_AudioSource.Play();
                }

                GameObject test = Instantiate(projectilePrefab, rootObject.transform.position, rootObject.transform.rotation);
                test.GetComponent<projectile>().Set(damage, 100, radius * 1.2f, 0, 1);
                CanShoot = true;
            }
        }
    }

    public void Update()
    {
        BuffedTower();
    }
}
