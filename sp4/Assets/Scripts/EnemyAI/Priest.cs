using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Priest : MonoBehaviour
{
    public List<Transform> target;
    public float totaldistance = 0;
    public float percentageofdistance = 0;
    public float currentdistanceontrack = 0;
    public int current;
    public Animator m_Animator;
    public bool CanShoot;
    public Enemy_AI enemy_AI;
    public string Name = "Priest";
    public int CashDrop = 300;
    public GameObject rootObject;
    public int speed = 6;
    public int ArmorType = 0;
    public int Damage = 3;
    public GameObject HealingFX;
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
        enemy_AI.ArmorType = 0;
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

    private void HealAll()
    {
        Collider[] overlaps = new Collider[999];
        int count = Physics.OverlapSphereNonAlloc(this.transform.position, enemy_AI.maxRadius, overlaps);
        if (count > 0)
        {
            for (int i = 0; i < count + 1; i++)
            {
                if (overlaps[i] != null)
                {
                    if (overlaps[i].tag == "Enemy") //If Target enters field of view
                    {
                        if (Vector3.Distance(overlaps[i].transform.position, transform.position) < enemy_AI.maxRadius)
                        {
                            HealingFX.transform.position = overlaps[i].gameObject.transform.position;
                            Instantiate(HealingFX);
                            overlaps[i].gameObject.GetComponent<Enemy_AI>().MinusHP(-10);
                        }
                    }
                }
            }
        }
    }
    public void Update()
    {
        //Transform target = Enemy_AI.GetQuaternionTarget(rootObject.transform, Enemy_AI.maxRadius);
        if (enemy_AI.GetQuaternionTarget(rootObject.transform, enemy_AI.maxRadius) != null && (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Walk") || 
            m_Animator.GetCurrentAnimatorStateInfo(0).IsName("idle01")) && CanShoot)
        {
            Debug.Log("Firing");
            Fire();
        }
        else if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Heal") && enemy_AI.GetQuaternionTarget(rootObject.transform, enemy_AI.maxRadius) != null)
        {
            if (m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f && !CanShoot)
            {
                if (enemy_AI.TargetObject != null)
                    HealAll();
                CanShoot = true;
            }
        }
        else if (FindDistance(transform, enemy_AI.TargetObject) > enemy_AI.maxRadius && !m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Heal")) //if there is no enemy and the distance
        {
            CanShoot = true;
            m_Animator.SetTrigger("Walking");
            if (Vector3.Distance(transform.position, target[current].position) > 0.1f) //target is waypoints
            {
                transform.LookAt(target[current]);
                transform.position += transform.forward * Time.deltaTime * speed;
                if (current > 0)
                {
                    currentdistanceontrack += Time.deltaTime * speed;
                    percentageofdistance = (currentdistanceontrack / totaldistance) * 100;
                    enemy_AI.CurrentPercentage = percentageofdistance;
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
    }
}
