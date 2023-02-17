using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public List<Transform> target;
    public int current;
    // Start is called before the first frame update
    void Start()
    {
        current = 0;
    }

    public void AssignWaypoints(GameObject WaypointGO)
    {
        target.Clear();
        foreach (Transform child in WaypointGO.transform)
        {
            target.Add(child.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, target[current].position) > 0.1f) //target is waypoints
        {
            transform.LookAt(target[current]);
            transform.position += transform.forward * Time.deltaTime * 10;
        }
        else
        {
            ++current;
            if (current >= target.Count)
            {
                current = 0;
            }
        }
    }
}
