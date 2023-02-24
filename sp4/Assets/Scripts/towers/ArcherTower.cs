using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ArcherTower : TowerBase
{
    AudioSource m_AudioSource;
    // Start is called before the first frame update
    void Start()
    {
        cost = 200;
        UpgradeCost = 225;
        CanShoot = true;
        damage = 10;
        attackSpd = 2;
        Lvl = 1;
        tower_AI = GetComponent<Tower_AI>();
        tower_AI.maxRadius = 10;
        tower_AI.HP = 100;
        tower_AI.HPSlider.maxValue = tower_AI.HP;
        m_AudioSource = GetComponent<AudioSource>();
        Name = "Archer";
    }

    public override void Fire()
    {
        m_Animator.SetTrigger("Attack");
        m_Animator.SetFloat("attackSpeed", attackSpd);
        CanShoot = false;
        // tower_AI.GetQuaternionTarget(rootObject.transform,radius);
    }
    public override void OnUpdate()
    {

        if (tower_AI.GetQuaternionTarget(rootObject.transform, tower_AI.maxRadius) != null && m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") && CanShoot)
        {
            Debug.Log("GOBLIN SHOOT");
            Fire();
        }
        else if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") && !CanShoot)
        {
            if (m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f)
            {
                if (!m_AudioSource.isPlaying)
                {
                    m_AudioSource.Play();
                }
                GameObject test = Instantiate(projectilePrefab, rootObject.transform.position, rootObject.transform.rotation);
                test.GetComponent<projectile>().Set(damage, 100, tower_AI.maxRadius * 1.2f, 0);
                CanShoot = true;
            }
        }
    }

    public void Update()
    {
        BuffedTower();
    }
}
