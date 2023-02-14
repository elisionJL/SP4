using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Base_Interaction : MonoBehaviour
{
    public float maxDistance = 5.0f;
    // Start is called before the first frame update
    public List<GameObject> Towers;
    public GameObject TowerToSpawn; //What player decides that they want to spawn

    private bool CanPlace = false;

    public GameObject UpgradePrompt, UpgradeUI;
    bool upgrade = false;

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
                && distance <= 3.0f && upgrade == false) //If object tag is Interactable and it's close to the player
                {
                    if(UpgradePrompt.activeSelf == false)
                        UpgradePrompt.SetActive(true);

                    if(Input.GetKeyDown(KeyCode.E))
                    {
                        UpgradeUI.SetActive(true);
                        UpgradePrompt.SetActive(false);

                        UpgradeUI.GetComponent<Upgrade_Tower>().GetTowerInfo(hit.collider.gameObject);
                        upgrade = true;
                        gameObject.GetComponent<Player>().UnlockMouse();
                    }
                }
                else
                {
                    UpgradePrompt.SetActive(false);
                }
            }
        }
        else
            Debug.DrawRay(origin, direction * maxDistance, Color.red);

        if(CanPlace == true && TowerToSpawn != null)
            Destroy(TowerToSpawn);

        //if(UpgradeUI.activeSelf == true && upgrade == true)
        //{
        //    if (Input.GetKeyDown(KeyCode.E))
        //        UpgradeUI.SetActive(false);
        //}
    }

    public void CloseShopUI()
    {
        upgrade = false;
    }
    public void SpawnObject(RaycastHit hit, float distance)
    {
        if (TowerToSpawn != null && CanPlace == false) //If there is an object to place and user didn't place it down yet
        {
            //If distance of tower from player is less than 5.0f and is a place that can be placed down onto
            if(distance <= 5.0f && hit.collider.gameObject.tag == "PlaceableArea")
            {
                TowerToSpawn.transform.position = new Vector3(hit.point.x, hit.point.y + (TowerToSpawn.transform.localScale.y / 2), hit.point.z);

                //Until user presses Left Mouse Trigger to place it down
                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("pressed click");
                    //Set it's final position from when user pressed E
                    TowerToSpawn.transform.position = new Vector3(hit.point.x, hit.point.y + (TowerToSpawn.transform.localScale.y / 2), hit.point.z);

                    //Enable the AI Code to start finding enemy
                    TowerToSpawn.gameObject.GetComponent<Tower_AI>().enabled = true;

                    if(TowerToSpawn.gameObject.GetComponent<MageTower>() != null)
                        TowerToSpawn.gameObject.GetComponent<MageTower>().enabled = true;
                    else if(TowerToSpawn.gameObject.GetComponent<DragonTower>() != null)
                        TowerToSpawn.gameObject.GetComponent<DragonTower>().enabled = true;
                    else if(TowerToSpawn.gameObject.GetComponent<SkeletonTower>() != null)
                        TowerToSpawn.gameObject.GetComponent<SkeletonTower>().enabled = true;
                    else if(TowerToSpawn.gameObject.GetComponent<DemonGirlTower>() != null)
                        TowerToSpawn.gameObject.GetComponent<DemonGirlTower>().enabled = true;
                    else if (TowerToSpawn.gameObject.GetComponent<ZombieTower>() != null)
                        TowerToSpawn.gameObject.GetComponent<ZombieTower>().enabled = true;
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

                        else if (TowerToSpawn.transform.GetChild(1).gameObject.GetComponent<SkinnedMeshRenderer>() != null)
                        {
                            //If Object has skinned mesh renderer, do th same as mesh renderer and change alpha material to 255 and rendering mode to opaque
                            TowerToSpawn.transform.GetChild(1).gameObject.GetComponent<SkinnedMeshRenderer>().material.color = new Color(TowerToSpawn.transform.GetChild(1).gameObject.GetComponent<SkinnedMeshRenderer>().material.color.r, TowerToSpawn.transform.GetChild(1).gameObject.GetComponent<SkinnedMeshRenderer>().material.color.g, TowerToSpawn.transform.GetChild(1).gameObject.GetComponent<SkinnedMeshRenderer>().material.color.b, 1);
                            SetTransparentToOpaque(TowerToSpawn.transform.GetChild(1).gameObject.GetComponent<SkinnedMeshRenderer>().material);
                        }

                        else if (TowerToSpawn.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>() != null)
                        {
                            //If Object has skinned mesh renderer, do th same as mesh renderer and change alpha material to 255 and rendering mode to opaque
                            TowerToSpawn.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>().material.color = new Color(TowerToSpawn.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>().material.color.r, TowerToSpawn.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>().material.color.g, TowerToSpawn.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>().material.color.b, 1);
                            SetTransparentToOpaque(TowerToSpawn.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>().material);
                        }

                        else if(TowerToSpawn.transform.GetChild(0).gameObject.transform.GetChild(4).GetComponent<SkinnedMeshRenderer>() != null)
                        {
                            TowerToSpawn.transform.GetChild(0).gameObject.transform.GetChild(4).gameObject.GetComponent<SkinnedMeshRenderer>().material.color = new Color(TowerToSpawn.transform.GetChild(0).gameObject.transform.GetChild(4).gameObject.GetComponent<SkinnedMeshRenderer>().material.color.r, TowerToSpawn.transform.GetChild(0).gameObject.transform.GetChild(4).gameObject.GetComponent<SkinnedMeshRenderer>().material.color.g, TowerToSpawn.transform.GetChild(0).gameObject.transform.GetChild(4).gameObject.GetComponent<SkinnedMeshRenderer>().material.color.b, 1);
                            SetTransparentToOpaque(TowerToSpawn.transform.GetChild(0).gameObject.transform.GetChild(4).gameObject.GetComponent<SkinnedMeshRenderer>().material);

                            TowerToSpawn.transform.GetChild(0).gameObject.transform.GetChild(5).gameObject.GetComponent<SkinnedMeshRenderer>().material.color = new Color(TowerToSpawn.transform.GetChild(0).gameObject.transform.GetChild(5).gameObject.GetComponent<SkinnedMeshRenderer>().material.color.r, TowerToSpawn.transform.GetChild(0).gameObject.transform.GetChild(5).gameObject.GetComponent<SkinnedMeshRenderer>().material.color.g, TowerToSpawn.transform.GetChild(0).gameObject.transform.GetChild(5).gameObject.GetComponent<SkinnedMeshRenderer>().material.color.b, 1);
                            SetTransparentToOpaque(TowerToSpawn.transform.GetChild(0).gameObject.transform.GetChild(5).gameObject.GetComponent<SkinnedMeshRenderer>().material);

                            TowerToSpawn.transform.GetChild(0).gameObject.transform.GetChild(6).gameObject.GetComponent<SkinnedMeshRenderer>().material.color = new Color(TowerToSpawn.transform.GetChild(0).gameObject.transform.GetChild(6).gameObject.GetComponent<SkinnedMeshRenderer>().material.color.r, TowerToSpawn.transform.GetChild(0).gameObject.transform.GetChild(6).gameObject.GetComponent<SkinnedMeshRenderer>().material.color.g, TowerToSpawn.transform.GetChild(0).gameObject.transform.GetChild(6).gameObject.GetComponent<SkinnedMeshRenderer>().material.color.b, 1);
                            SetTransparentToOpaque(TowerToSpawn.transform.GetChild(0).gameObject.transform.GetChild(6).gameObject.GetComponent<SkinnedMeshRenderer>().material);

                            TowerToSpawn.transform.GetChild(0).gameObject.transform.GetChild(7).gameObject.GetComponent<SkinnedMeshRenderer>().material.color = new Color(TowerToSpawn.transform.GetChild(0).gameObject.transform.GetChild(7).gameObject.GetComponent<SkinnedMeshRenderer>().material.color.r, TowerToSpawn.transform.GetChild(0).gameObject.transform.GetChild(7).gameObject.GetComponent<SkinnedMeshRenderer>().material.color.g, TowerToSpawn.transform.GetChild(0).gameObject.transform.GetChild(7).gameObject.GetComponent<SkinnedMeshRenderer>().material.color.b, 1);
                            SetTransparentToOpaque(TowerToSpawn.transform.GetChild(0).gameObject.transform.GetChild(7).gameObject.GetComponent<SkinnedMeshRenderer>().material);

                            TowerToSpawn.transform.GetChild(0).gameObject.transform.GetChild(8).gameObject.GetComponent<SkinnedMeshRenderer>().material.color = new Color(TowerToSpawn.transform.GetChild(0).gameObject.transform.GetChild(8).gameObject.GetComponent<SkinnedMeshRenderer>().material.color.r, TowerToSpawn.transform.GetChild(0).gameObject.transform.GetChild(8).gameObject.GetComponent<SkinnedMeshRenderer>().material.color.g, TowerToSpawn.transform.GetChild(0).gameObject.transform.GetChild(8).gameObject.GetComponent<SkinnedMeshRenderer>().material.color.b, 1);
                            SetTransparentToOpaque(TowerToSpawn.transform.GetChild(0).gameObject.transform.GetChild(8).gameObject.GetComponent<SkinnedMeshRenderer>().material);
                        }
                    }

                    //Lastly, instantiate a clone of the "ghost" object
                    if (TowerToSpawn.gameObject.GetComponent<MageTower>() != null && gameObject.GetComponent<Player>().MinusSouls(400) == true)
                        Instantiate(TowerToSpawn);
                    else if (TowerToSpawn.gameObject.GetComponent<DragonTower>() != null && gameObject.GetComponent<Player>().MinusSouls(500) == true)
                        Instantiate(TowerToSpawn);
                    else if (TowerToSpawn.gameObject.GetComponent<SkeletonTower>() != null && gameObject.GetComponent<Player>().MinusSouls(200) == true)
                        Instantiate(TowerToSpawn);
                    else if (TowerToSpawn.gameObject.GetComponent<ZombieTower>() != null && gameObject.GetComponent<Player>().MinusSouls(200) == true)
                        Instantiate(TowerToSpawn);
                    else if (TowerToSpawn.gameObject.GetComponent<DemonGirlTower>() != null && gameObject.GetComponent<Player>().MinusSouls(100) == true)
                        Instantiate(TowerToSpawn);

                    //Then delete the Ghost object
                    Destroy(TowerToSpawn);

                    //Before setting CanPlace bool to true
                    CanPlace = true;
                }
                
                //Otherwise if user presses right mouse trigger instead
                else if(Input.GetKeyDown(KeyCode.Mouse1))
                {
                    //Then delete the Ghost object
                    Destroy(TowerToSpawn);
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
