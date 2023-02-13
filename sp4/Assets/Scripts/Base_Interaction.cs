using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_Interaction : MonoBehaviour
{
    public float maxDistance = 5.0f;
    // Start is called before the first frame update
    public List<GameObject> Towers;
    GameObject TowerToSpawn; //What player decides that they want to spawn

    bool CanPlace = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;
        Ray ray = new Ray(origin, direction);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) //if Object is hit
        {
            float distance = hit.distance; //Get the distance from the ray to the hit object

            Debug.DrawRay(origin, direction * distance, Color.red);

            if (Input.GetKeyDown(KeyCode.P)) //Debug Test Spawn
            {
                if (!CanPlace) //
                {
                    Debug.Log("Hit");
                    Towers[0].transform.position = new Vector3(hit.point.x, hit.point.y + (Towers[0].transform.localScale.y / 2), hit.point.z);
                    Towers[0].gameObject.GetComponent<Tower_AI>().enabled = true;
                    Instantiate(Towers[0]);
                    CanPlace = true;
                }
                else
                    CanPlace = false;
            }

            if (!CanPlace) //Not Place Object
            {
                if (hit.transform.gameObject.tag == "interactable"
                && distance <= 3.0f) //If object tag is Interactable and it's close to the player
                    Debug.Log("Press E to Interact");
            }
            else //If Place Object
            {
            }
            
        }
        else
            Debug.DrawRay(origin, direction * maxDistance, Color.red);
    }
}
