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

            SpawnObject(hit, distance);

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

    public void SpawnObject(RaycastHit hit, float distance)
    {
        if (TowerToSpawn != null && CanPlace == false) //If there is an object to place and user didn't place it down yet
        {
            //If distance of tower from player is less than 5.0f and is a place that can be placed down onto
            if(distance <= 5.0f && hit.collider.gameObject.tag == "PlaceableArea")
            {
                //Move the objects position to wherever the raycast has hit
                TowerToSpawn.transform.position = new Vector3(hit.point.x, hit.point.y + (TowerToSpawn.transform.localScale.y / 2), hit.point.z);

                //Until user presses E to place it down
                if (Input.GetKeyDown(KeyCode.E))
                {
                    //Set it's final position from when user pressed E
                    TowerToSpawn.transform.position = new Vector3(hit.point.x, hit.point.y + (TowerToSpawn.transform.localScale.y / 2), hit.point.z);

                    //Enable the AI Code to start finding enemy
                    TowerToSpawn.gameObject.GetComponent<Tower_AI>().enabled = true;
                    TowerToSpawn.gameObject.GetComponent<TestTower>().enabled = true;

                    //Turn on box collision
                    TowerToSpawn.gameObject.GetComponent<BoxCollider>().enabled = true;

                    //Check if object has mesh renderer
                    if (TowerToSpawn.gameObject.GetComponent<MeshRenderer>() != null)
                    {
                        //If Yes, set material alpha of mesh renderer from 128 to 255 then set it's rendering mode to opaque
                        TowerToSpawn.gameObject.GetComponent<MeshRenderer>().material.color = new Color(TowerToSpawn.gameObject.GetComponent<MeshRenderer>().material.color.r, TowerToSpawn.gameObject.GetComponent<MeshRenderer>().material.color.g, TowerToSpawn.gameObject.GetComponent<MeshRenderer>().material.color.b, 1);
                        SetTransparentToOpaque(TowerToSpawn.gameObject.GetComponent<MeshRenderer>().material);
                    }
                    else //Otherwise
                    {
                        //Check if object has skinned mesh renderer or
                        if (TowerToSpawn.transform.GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>() != null)
                        {
                            //If Object has skinned mesh renderer, do th same as mesh renderer and change alpha material to 255 and rendering mode to opaque
                            TowerToSpawn.transform.GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>().material.color = new Color(TowerToSpawn.transform.GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>().material.color.r, TowerToSpawn.transform.GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>().material.color.g, TowerToSpawn.transform.GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>().material.color.b, 1);
                            SetTransparentToOpaque(TowerToSpawn.transform.GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>().material);
                        }
                    }

                    //Lastly, instantiate a clone of the "ghost" object
                    Instantiate(TowerToSpawn);

                    //Then delete the Ghost object
                    Destroy(TowerToSpawn);

                    //Before setting CanPlace bool to true
                    CanPlace = true;
                }
            }
        }
        else if (TowerToSpawn == null)
            CanPlace = false;
    }

    public void SetTransparentToOpaque(Material ObjectToOpaque)
    {
        // Set the render queue of the material to (int)UnityEngine.Rendering.RenderQueue.Geometry, 
        // which indicates that the material should be rendered as opaque.
        ObjectToOpaque.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Geometry;

        // Set the source blend mode of the material to UnityEngine.Rendering.BlendMode.One,
        // which indicates that the source color should be used without modification.
        ObjectToOpaque.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);

        // Set the destination blend mode of the material to UnityEngine.Rendering.BlendMode.Zero,
        // which indicates that the destination color should be ignored.
        ObjectToOpaque.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);

        // Set the ZWrite property of the material to 1, which indicates that the material
        // should write to the depth buffer.
        ObjectToOpaque.SetInt("_ZWrite", 1);

        // Disable the "_ALPHATEST_ON" keyword for the material, which indicates that the
        // material should not use an alpha test.
        ObjectToOpaque.DisableKeyword("_ALPHATEST_ON");

        // Disable the "_ALPHABLEND_ON" keyword for the material, which indicates that the
        // material should not use alpha blending.
        ObjectToOpaque.DisableKeyword("_ALPHABLEND_ON");

        // Disable the "_ALPHAPREMULTIPLY_ON" keyword for the material, which indicates that
        // the material should not use alpha premultiplication.
        ObjectToOpaque.DisableKeyword("_ALPHAPREMULTIPLY_ON");
    }
}
