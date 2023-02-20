using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWave : MonoBehaviour
{
    public Torus torus;
    // Start is called before the first frame update
    void Start()
    {
        torus.NewMesh();
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.localScale.x < 550)
        {
            Vector3 CurrScale = transform.localScale;
            transform.localScale = new Vector3(CurrScale.x + 1.5f, CurrScale.y + 1.6f, CurrScale.z + 1.5f);
            transform.Rotate(0, 500 * Time.deltaTime, 0);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
