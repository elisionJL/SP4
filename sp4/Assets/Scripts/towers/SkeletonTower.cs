using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class SkeletonTower : TowerBase
{
    AudioSource m_AudioSource;
    // Start is called before the first frame update
    void Start()
    {
        CanShoot = true;
        damage = 10;
        attackSpd = 1;
        Lvl = 1;
        radius = 10;
        tower_AI = GetComponent<Tower_AI>();
        tower_AI.maxRadius = 7.5f;
        tower_AI.HP = 100;
        tower_AI.HPSlider.maxValue = tower_AI.HP;
        tower_AI.targetingMode = Tower_AI.TARGETING.CLOSEST;
        Name = "Skeleton";
        cost = 200;
        UpgradeCost = 100;
        m_AudioSource = GetComponent<AudioSource>();
    }

    public override void Fire()
    {
        m_Animator.SetTrigger("Attack");
        m_Animator.SetFloat("attackSpeed", attackSpd);
        CanShoot = false;
    }
    public override void OnUpdate()
    {
        Transform target = tower_AI.GetQuaternionTarget(rootObject.transform, tower_AI.maxRadius);
        //Transform target = tower_AI.GetQuaternionTarget(rootObject.transform, tower_AI.maxRadius);
        if (target != null && m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Skeleton@Idle01") && CanShoot)
        {
            Fire();
        }
        else if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Skeleton@Attack01") && !CanShoot)
        {
            if (m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.3f)
            {
                if (tower_AI.playerTransform)
                {
                    if (!m_AudioSource.isPlaying)
                    {
                        m_AudioSource.Play();
                    }
                    //hit the enemy here
                    tower_AI.playerTransform.gameObject.GetComponent<Enemy_AI>().MinusHP(damage);
                }
                CanShoot = true;
            }
        }
    }

    public void Update()
    {
        BuffedTower();
    }
}
