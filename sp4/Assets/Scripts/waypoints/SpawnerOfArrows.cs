using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerOfArrows : MonoBehaviour
{
    public List<Transform> Waypoints;
    private float timer;
    public GameObject Arrows;
    public GameObject ArrowContainer;
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
        timer += Time.deltaTime;
        if (timer > 2)
        {
            for (int i = 0; i < Waypoints.Count - 1; ++i)
            {
                Arrows.transform.position = Waypoints[i].position;
                Arrows.GetComponent<Arrow>().AssignWaypoints(Waypoints[i + 1]);
                Instantiate(Arrows).transform.SetParent(ArrowContainer.transform);
            }
            timer = 0;
        }
    }
}
