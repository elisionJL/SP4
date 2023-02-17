using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerOfArrows : MonoBehaviour
{
    public List<Transform> Waypoints;
    // Start is called before the first frame update
    void Start()
    {
        int amountofchildren = transform.childCount;
        for (int i = 0; i < amountofchildren; ++i)
        {
            Waypoints.Add(gameObject.transform.GetChild(i));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
