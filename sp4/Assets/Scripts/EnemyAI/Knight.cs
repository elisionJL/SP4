using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Knight : MonoBehaviour
{
    public List<Transform> target;
    public float totaldistance = 0;
    public float percentageofdistance = 0;
    public float currentdistanceontrack = 0;
    public int current;
    public Animator m_Animator;
    public bool CanShoot;
    public Enemy_AI enemy_AI;
    public string Name = "Knight";
    public int CashDrop = 1000;
    public GameObject rootObject;
    public int speed = 10;
    public int Damage = 30;
    public GameObject DebuffFX;
    float DebuffFXSpawnTime = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        target = enemy_AI.ReturnWaypoints();
        for (int i = 0; i < target.Count - 1; ++i)
        {
            totaldistance += Vector3.Distance(target[i].position, target[i + 1].position);
        }
        current = 0;
        CanShoot = true;
        //enemy_AI.HP = 100;
        enemy_AI.ArmorType = 1;
    }

    public void Fire()
    {
        m_Animator.SetTrigger("Attack");
        CanShoot = false;
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
        //Transform target = Enemy_AI.GetQuaternionTarget(rootObject.transform, Enemy_AI.maxRadius);
        if (enemy_AI.GetQuaternionTarget(rootObject.transform, enemy_AI.maxRadius) != null && (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") || 
            m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Walk")) && CanShoot)
        {
            Fire();
        }
        else if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") && !CanShoot)
        {
            if (m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.45f)
            {
                //hit the enemy here
                if (enemy_AI.TargetObject != null)
                    enemy_AI.TargetObject.gameObject.GetComponent<Tower_AI>().MinusHP(Damage);
                CanShoot = true;
            }
        }
        else if (FindDistance(transform, enemy_AI.TargetObject) > enemy_AI.maxRadius) //if there is no enemy and the distance
        {
            m_Animator.SetTrigger("Walk");
            if (Vector3.Distance(transform.position, target[current].position) > 0.1f) //target is waypoints
            {
                transform.LookAt(target[current]);
                transform.position += transform.forward * Time.deltaTime * speed;
                if (current > 0)
                {
                    currentdistanceontrack += Time.deltaTime * speed;
                    percentageofdistance = (currentdistanceontrack / totaldistance) * 100;
                }
                else
                {
                    currentdistanceontrack = 0;
                    percentageofdistance = 0;
                }
            }
            else
            {
                ++current;
                if (current >= target.Count)
                {
                    current = 0;
                }
            }
        }

        #region ToBeTested
        if (gameObject.GetComponent<Enemy_AI>().GetEnemyDebuff() == true)
        {
            speed = 7;
            Damage = 5;

            DebuffFXSpawnTime -= 1f * Time.deltaTime;

            if (DebuffFXSpawnTime <= 0f)
            {
                Instantiate(DebuffFX, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                DebuffFXSpawnTime = 2.5f;
            }
        }
        else
        {
            speed = 15;
            Damage = 10;

            DebuffFXSpawnTime = 2.5f;
        }
        #endregion
    }
}
