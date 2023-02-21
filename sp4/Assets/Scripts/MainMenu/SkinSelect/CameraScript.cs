using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.mouseScrollDelta.y > 0 || Input.GetKey(KeyCode.W))&& Vector3.Distance(transform.position, Player.transform.position) > 2)
        {
            transform.position += transform.forward * Time.deltaTime * 10;
        }
        else if ((Input.mouseScrollDelta.y < 0 || Input.GetKey(KeyCode.S)) && Vector3.Distance(transform.position, Player.transform.position) < 10)
        {
            transform.position -= transform.forward * Time.deltaTime * 10;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.RotateAround(Player.transform.position, Vector3.up, Time.deltaTime * 30);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.RotateAround(Player.transform.position, Vector3.up, Time.deltaTime * -30);
        }
        else
            transform.RotateAround(Player.transform.position, Vector3.up, Time.deltaTime * 5);

        transform.LookAt(Player.transform.position, Vector3.up);
    }
}
