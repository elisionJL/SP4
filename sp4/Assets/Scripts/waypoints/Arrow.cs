using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void AssignWaypoints(GameObject WaypointGO)
    {
        target = WaypointGO.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, target.position) > 0.1f) //target is waypoints
        {
            transform.LookAt(target);
            transform.position += transform.forward * Time.deltaTime * 10;
        }
        else
        {
            Destroy(this);
        }
    }
}
