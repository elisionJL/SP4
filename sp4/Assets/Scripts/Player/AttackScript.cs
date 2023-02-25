using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy") //If the collider that's been hit has the tag of enemy
        {
            other.gameObject.GetComponent<Enemy_AI>().MinusHP(25); //Damage said enemy
            gameObject.GetComponent<AttackScript>().enabled = false;
        }
    }
}

