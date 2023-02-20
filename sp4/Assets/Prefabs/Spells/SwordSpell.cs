using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSpell : MonoBehaviour
{
    public Vector3 Destination;
    private float speed = 30;
    public GameObject Explosion;
    public GameObject ExplosionRadius;
    private float countup = -1;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 400, 0);
        countup = -1;
    }

    public void SetDestination (Vector3 LandHere)
    {
        Destination = LandHere;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(Destination, transform.position) > 1)
        {
            speed += 0.5f;
            transform.LookAt(Destination);
            transform.position += transform.forward * Time.deltaTime * speed;
            float currentx = transform.rotation.x;
            float currenty = transform.rotation.y;
            float currentz = transform.rotation.z;
            transform.rotation = Quaternion.Euler(currentx + 180, currenty + 90, currentz);
        }
        else
        {
            if (countup == -1)
            {
                Explosion.transform.position = transform.position;
                Instantiate(Explosion);
                ExplosionRadius.transform.position = transform.position;
                Instantiate(ExplosionRadius);
                countup = 0.0f;
                
            }
            if (countup < 0.5)
                countup += Time.deltaTime;
            else
                Destroy(gameObject);
        }
    }
}
