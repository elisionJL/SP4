 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public int Type;
    public int damage;
    public float speed;
    public float distance;
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
        Type = 0;
    }
    public void Set(int dmg, float spd,float dist, int From)
    {
        damage = dmg;
        speed = spd;
        distance = dist;
        Type = From;
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
        if (other.tag != "Player" && other.tag != "interactable")
        {
            if (Type == 0 && other.tag == "Enemy")
            {
                other.gameObject.GetComponent<Enemy_AI>().MinusHP(damage);
            }
            Destroy(gameObject);
        }
    }
}
