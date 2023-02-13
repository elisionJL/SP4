using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_Interaction : MonoBehaviour
{
    public float maxDistance = 5.0f;
    // Start is called before the first frame update
    public List<GameObject> Towers;
    public GameObject TowerToSpawn; //What player decides that they want to spawn

    private bool CanPlace = false;

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

            SpawnObject(hit);

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

    public void SpawnObject(RaycastHit hit)
    {
        if (TowerToSpawn != null && CanPlace == false)
        {
            TowerToSpawn.transform.position = new Vector3(hit.point.x, hit.point.y + (TowerToSpawn.transform.localScale.y / 2), hit.point.z);
            if (Input.GetKeyDown(KeyCode.E))
            {
                TowerToSpawn.gameObject.GetComponent<Tower_AI>().enabled = true;
                TowerToSpawn.gameObject.GetComponent<TestTower>().enabled = true;
                CanPlace = true;
                TowerToSpawn.transform.position = new Vector3(hit.point.x, hit.point.y + (TowerToSpawn.transform.localScale.y / 2), hit.point.z);
                TowerToSpawn.gameObject.GetComponent<BoxCollider>().enabled = true;
                if(TowerToSpawn.gameObject.GetComponent<MeshRenderer>() != null)
                    TowerToSpawn.gameObject.GetComponent<MeshRenderer>().material.color = new Color(TowerToSpawn.gameObject.GetComponent<MeshRenderer>().material.color.r, TowerToSpawn.gameObject.GetComponent<MeshRenderer>().material.color.g, TowerToSpawn.gameObject.GetComponent<MeshRenderer>().material.color.b, 1);
                else
                {
                    if(TowerToSpawn.transform.GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>() != null)
                        TowerToSpawn.transform.GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>().material.color = new Color(TowerToSpawn.transform.GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>().material.color.r, TowerToSpawn.transform.GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>().material.color.g, TowerToSpawn.transform.GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>().material.color.b, 1);
                }
                Instantiate(TowerToSpawn);
                Destroy(TowerToSpawn);
            }
        }
        else if (TowerToSpawn == null)
            CanPlace = false;
    }
}
