using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BudgetExplosionHandler : MonoBehaviour
{
    private bool changeview;
    private Vector3 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        changeview = false;
        startPosition = Camera.main.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (changeview)
        {
            Camera.main.transform.position = startPosition + Random.insideUnitSphere;
            changeview = false;
        }
        else
        {
            Camera.main.transform.position = startPosition;
            changeview = true;
        }

        if (transform.localScale.x < 100)
        {
            Vector3 CurrScale = transform.localScale;
            transform.localScale = new Vector3(CurrScale.x + 0.3f, CurrScale.y + 0.3f, CurrScale.z + 0.3f);
            transform.Rotate(0, 500 * Time.deltaTime, 0);
        }
        else
        {
            Camera.main.transform.position = startPosition;
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
