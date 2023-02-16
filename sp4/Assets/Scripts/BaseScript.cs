using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BaseScript : MonoBehaviour
{
    [SerializeField]
    private int HP = 100;
    private GameObject HP_Bar;

    private void Start()
    {
        HP_Bar = gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        if (HP <= 0)
            SceneManager.LoadScene("LoseScene");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            HP -= 10;
            HP_Bar.GetComponent<Slider>().value = HP;
            Destroy(other.gameObject);
        }
    }
}
