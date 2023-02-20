using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Bear : MonoBehaviour
{
    public List<Transform> target;
    public float totaldistance = 0;
    public float percentageofdistance = 0;
    public float currentdistanceontrack = 0;
    public int current;
    public Animator m_Animator;
    public bool CanShoot;
    public Enemy_AI enemy_AI;
    public string Name = "Bear";
    public int CashDrop = 400;
    public GameObject rootObject;
    public int speed = 5;
    public int Damage = 10;
    public bool CanRun = true;
    public GameObject CavalierKnight;
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
        CanRun = true;
        speed = 15;
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
        //check if there is an enemy in the radius ,if there is ,trigger animation 
        if (enemy_AI.TargetObject != null)
        {
            m_Animator.SetTrigger("Idle");
            if (CavalierKnight.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("infantry_01_idle"))
            {
                if (CanRun)
                {
                    CavalierKnight.GetComponent<Cavalier>().Fire(enemy_AI.TargetObject.gameObject, Damage * 2);
                    CanRun = false;
                    speed = 9;
                }
                else
                {
                    CavalierKnight.GetComponent<Cavalier>().Fire(enemy_AI.TargetObject.gameObject, Damage);
                }
            }
        }
        //else walk to the next waypoint
        else if (enemy_AI.TargetObject == null && FindDistance(transform, enemy_AI.TargetObject) > enemy_AI.maxRadius)
        {
            if (CanRun)
                m_Animator.SetTrigger("Run");
            else
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

            if(DebuffFXSpawnTime <= 0f)
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
