using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionForMeteors : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.gameObject.GetComponent<Enemy_AI>().MinusHP(50);
        }
    }
}
