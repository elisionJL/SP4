 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public int damage;
    public float speed;
    public float distance;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Set(int dmg, float spd,float dist)
    {
        damage = dmg;
        speed = spd;
        distance = dist;
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
        if(other.tag != "Player" && other.tag != "interactable")
        {       
            Destroy(gameObject);
        }
    }
}
