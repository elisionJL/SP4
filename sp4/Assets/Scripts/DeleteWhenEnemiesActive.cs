using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteWhenEnemiesActive : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount > 0)
        {
            if (GameObject.FindGameObjectsWithTag("Enemy").Length > 0)
            {
                for (int i = 0; i < transform.childCount; ++i)
                {
                    Destroy(transform.GetChild(i).gameObject);
                }
            }
        }
    }
}
