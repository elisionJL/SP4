using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BudgetExplosionHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.x < 150)
        {
            Vector3 CurrScale = transform.localScale;
            transform.localScale = new Vector3(CurrScale.x + 0.3f, CurrScale.y + 0.3f, CurrScale.z + 0.3f);
            transform.Rotate(0, 500 * Time.deltaTime, 0);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<Enemy_AI>().MinusHP(50);
        }
    }
}
