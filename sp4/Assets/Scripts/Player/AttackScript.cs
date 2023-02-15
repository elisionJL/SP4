using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.gameObject.GetComponent<Enemy_AI>().MinusHP(25);
            gameObject.GetComponent<AttackScript>().enabled = false;
        }
    }
}

