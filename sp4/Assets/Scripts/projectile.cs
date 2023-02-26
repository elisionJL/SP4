 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public int Source;
    public int damage;
    public float speed;
    public float distance;
    public int ProjectileType;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("created");
    }
    public void Set(int dmg, float spd,float dist)
    {
        damage = dmg;
        speed = spd;
        distance = dist;
        Source = 0; //0 is from tower and 1 is from enemy
        ProjectileType = 0;
    }
    public void Set(int dmg, float spd,float dist, int From)
    {
        damage = dmg;
        speed = spd;
        distance = dist;
        Source = From;
        ProjectileType = 0;
    }
    public void Set(int dmg, float spd,float dist, int From, int Type)
    {
        damage = dmg;
        speed = spd;
        distance = dist;
        Source = From;
        ProjectileType = Type;
    }
    // Update is called once per frame
    void Update()
    {
        distance -= Time.deltaTime * speed;
        if (distance  < 0)
        {
            Destroy(gameObject);
        }
        transform.position += transform.forward * Time.deltaTime * speed;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player" && other.tag != "interactable" && other.tag != "OOB")
        {
            if (Source <= 0 && other.tag == "Enemy")
                {
                if (ProjectileType >= other.gameObject.GetComponent<Enemy_AI>().ArmorType)
                    other.gameObject.GetComponent<Enemy_AI>().MinusHP(damage);
            }
            if(Source == -1)
            {
                GetComponent<FireBall>().Explode(transform);
            }
            Destroy(gameObject);
        }
    }
}
