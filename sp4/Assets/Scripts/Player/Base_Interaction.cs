using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Base_Interaction : MonoBehaviour
{
    public float maxDistance = 5.0f;
    // Start is called before the first frame update
    private void Start()
    {

    }

    #region Towers
    public List<GameObject> Towers;
    public GameObject TowerToSpawn, CurrentTowerLookAt; //What player decides that they want to spawn
    public GameObject EnemyToSpawn;
    GameObject CanvasToGet;
    private bool CanPlace = false;
    public GameObject Particlewhenplaced;
    #endregion

    #region Tower Upgrade
    public GameObject UpgradePrompt, UpgradeUI;
    bool upgrade = false;
    #endregion

    #region Player Attack
    public GameObject PlayerSword, PlayerCharacter;
    private bool Attack = false, Attack_Dir;
    private float AttackTime;
    private int random = 0;
    #endregion

    #region Hostage Panel
    public GameObject HostageUI;
    #endregion
    // Update is called once per frame
    void Update()
    {
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;
        Ray ray = new Ray(origin, direction);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && upgrade == false) //if Object is hit
        {
            float distance = hit.distance; //Get the distance from the ray to the hit object

            Debug.DrawRay(origin, direction * distance, Color.red);

            SpawnObject(hit, distance);

            if (!CanPlace) //Not Place Object
            {
                if (hit.transform.gameObject.tag == "interactable"
                && distance <= 3.0f && upgrade == false) //If object tag is Interactable and it's close to the player
                {
                    if (UpgradePrompt.activeSelf == false) //If the upgrade prompt is not yet actuve
                    {
                        CurrentTowerLookAt = hit.collider.gameObject; //Get the tower that is hit by the raycast

                        UpgradePrompt.SetActive(true); //Show prompt that tells player to press 'E' to upgrade
                        hit.transform.gameObject.GetComponent<Tower_AI>().Canvas.SetActive(true); //Show the hp bar on top of the tower
                        CanvasToGet = hit.transform.gameObject.GetComponent<Tower_AI>().Canvas; //Get the Canvas reference from said tower
                    }

                    //If no more towers are being hit and current tower being looked at DOES exist
                    if (CurrentTowerLookAt != hit.collider.gameObject && CurrentTowerLookAt != null)
                    {
                        //Then disable the upgrade prompt
                        UpgradePrompt.SetActive(false);
                        if (CurrentTowerLookAt.gameObject.GetComponent<Tower_AI>()) //Stop showing the hp bar from said tower
                            CurrentTowerLookAt.gameObject.GetComponent<Tower_AI>().Canvas.SetActive(false);
                    }
                    if (Input.GetKeyDown(KeyCode.E)) //When Player presses the E button
                    {
                        CurrentTowerLookAt = null; //Get rid of the tower reference
                        Destroy(CurrentTowerLookAt); //Then Destroy it
                        DisableSword(); //And disable the sword if it is being used

                        //Then show the shop upgrade ui while disabling the prompt
                        UpgradeUI.SetActive(true);
                        UpgradePrompt.SetActive(false);

                        //Get the towers name, cost, hp, atk, and future hp/atk after it's upgraded
                        UpgradeUI.GetComponent<Upgrade_Tower>().GetTowerInfo(hit.collider.gameObject);
                        upgrade = true; //Set bool that prevents player from moving when in upgrade shop to true
                        gameObject.GetComponent<Player>().UnlockMouse(); //Let the player use the mouse to navigate through the ui
                    }
                    if (Input.GetKeyDown(KeyCode.C)) //If player presses c instead
                    {
                        hit.transform.gameObject.GetComponent<Tower_AI>().UpdateTargeting(); //Change how the targeting of the tower works 
                    }
                }
            }
        }
        else //Code to get rid of current tower being looked at
        {
            UpgradePrompt.SetActive(false); 

            if (CanvasToGet != null && CanvasToGet.activeSelf == true)
                CanvasToGet.SetActive(false);

            CurrentTowerLookAt = null;
            Destroy(CurrentTowerLookAt);
        }

        if(CanPlace == true && TowerToSpawn != null) //If tower has been placed but tower hasn't been destroyed yet
            Destroy(TowerToSpawn); //Destroy it

        if (TowerToSpawn == null && CanPlace == false) //If player is able to attack then attack
            Player_Attack();

        if(Input.GetKeyDown(KeyCode.H)) //If Player presses H
        {
            DisableSword(); //Disable the sword and display the bribery menu

            HostageUI.SetActive(true);
            upgrade = true;
            gameObject.GetComponent<Player>().UnlockMouse();
        }

        //if(UpgradeUI.activeSelf == true && upgrade == true)
        //{
        //    if (Input.GetKeyDown(KeyCode.E))
        //        UpgradeUI.SetActive(false);
        //}
    }

    public void DisableSword() //Function to reset the sword to original position
    {
        PlayerSword.transform.GetChild(0).GetComponent<TrailRenderer>().Clear();
        PlayerSword.transform.GetChild(1).GetComponent<TrailRenderer>().Clear();
        PlayerSword.transform.localPosition = new Vector3(1.5f, 0f, 0.22f);
        PlayerSword.transform.localRotation = Quaternion.Euler(90, 0, -90);
        PlayerSword.SetActive(false);
    }

    public void CloseShopUI() //Function to let player move again if upgrade menu is closed
    {
        upgrade = false;
    }

    public void SpawnObject(RaycastHit hit, float distance)
    {
        if (TowerToSpawn != null && CanPlace == false) //If there is an object to place and user didn't place it down yet
        {
            //Disable the sword if player is currently attacking and tower is spawned
            DisableSword();

            //If distance of tower from player is less than 5.0f and is a place that can be placed down onto
            if (distance <= 5.0f && hit.collider.gameObject.tag == "PlaceableArea")
            {
                TowerToSpawn.transform.position = new Vector3(hit.point.x, hit.point.y + (TowerToSpawn.transform.localScale.y / 2), hit.point.z);

                //Until user presses Left Mouse Trigger to place it down
                if (Input.GetMouseButtonDown(0))
                {
                    #region Towers Component
                    DragonTower Dragon_Tower = null;
                    MageTower Mage_Tower = null;
                    SkeletonTower Skeleton_Tower = null;
                    DemonGirlTower Demon_Girl_Tower = null;
                    ZombieTower Zombie_Tower = null;
                    ArcherTower Archer_Tower = null;
                    GroundDragonTower Ground_Dragon_Tower = null;
                    SoulGrinderTower Soul_Grinder_Tower = null;

                    #endregion
                    //Debug.Log("pressed click");
                    //Set it's final position from when user pressed E
                    TowerToSpawn.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);

                    //Enable the AI Code to start finding enemy
                    TowerToSpawn.gameObject.GetComponent<Tower_AI>().enabled = true;

                    if(TowerToSpawn.gameObject.GetComponent<MageTower>() != null) //Mage
                    {
                        Mage_Tower = TowerToSpawn.gameObject.GetComponent<MageTower>();
                        Mage_Tower.enabled = true;
                    }
                    else if(TowerToSpawn.gameObject.GetComponent<DragonTower>() != null) //Dragon
                    {
                        Dragon_Tower = TowerToSpawn.gameObject.GetComponent<DragonTower>();
                        Dragon_Tower.enabled = true;
                    }
                    else if(TowerToSpawn.gameObject.GetComponent<SkeletonTower>() != null) //Skeleton
                    {
                        Skeleton_Tower = TowerToSpawn.gameObject.GetComponent<SkeletonTower>();
                        Skeleton_Tower.enabled = true;
                    }
                    else if(TowerToSpawn.gameObject.GetComponent<DemonGirlTower>() != null) //Succubus
                    {
                        Demon_Girl_Tower = TowerToSpawn.gameObject.GetComponent<DemonGirlTower>();
                        Demon_Girl_Tower.enabled = true;
                    }
                    else if (TowerToSpawn.gameObject.GetComponent<ZombieTower>() != null) //Zombie
                    {
                        Zombie_Tower = TowerToSpawn.gameObject.GetComponent<ZombieTower>();
                        Zombie_Tower.enabled = true;
                    }
                    else if (TowerToSpawn.gameObject.GetComponent<ArcherTower>() != null) //Archer
                    {
                        Archer_Tower = TowerToSpawn.gameObject.GetComponent<ArcherTower>();
                        Archer_Tower.enabled = true;
                    }
                    else if (TowerToSpawn.gameObject.GetComponent<GroundDragonTower>() != null) //Ground Dragon
                    {
                        Ground_Dragon_Tower = TowerToSpawn.gameObject.GetComponent<GroundDragonTower>();
                        Ground_Dragon_Tower.enabled = true;
                    }
                    else if (TowerToSpawn.gameObject.GetComponent<SoulGrinderTower>() != null) //Money Maker
                    {
                        Soul_Grinder_Tower = TowerToSpawn.gameObject.GetComponent<SoulGrinderTower>();
                        Soul_Grinder_Tower.enabled = true;
                    }

                    //Turn on box collision
                    TowerToSpawn.gameObject.GetComponent<BoxCollider>().enabled = true;

                    //Check if object has mesh renderer
                    if (TowerToSpawn.gameObject.GetComponent<MeshRenderer>() != null)
                    {
                        //If Yes, set material alpha of mesh renderer from 128 to 255 then set it's rendering mode to opaque
                        TowerToSpawn.gameObject.GetComponent<MeshRenderer>().material.color = new Color(TowerToSpawn.gameObject.GetComponent<MeshRenderer>().material.color.r, TowerToSpawn.gameObject.GetComponent<MeshRenderer>().material.color.g, TowerToSpawn.gameObject.GetComponent<MeshRenderer>().material.color.b, 1);
                        SetTransparentToOpaque(TowerToSpawn.gameObject.GetComponent<MeshRenderer>().material);

                        if (Soul_Grinder_Tower != null)
                        {
                            TowerToSpawn.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material.color = new Color(TowerToSpawn.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material.color.r, TowerToSpawn.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material.color.g, TowerToSpawn.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material.color.b, 1);
                            SetTransparentToOpaque(TowerToSpawn.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material);
                        }
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
                            if (Demon_Girl_Tower != null) //Succubus
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
                    }

                    TowerToSpawn.gameObject.tag = "interactable";

                    //Lastly, instantiate a clone of the "ghost" object
                    if (Mage_Tower != null && gameObject.GetComponent<Player>().MinusSouls(250) == true) //Mage 
                        Instantiate(TowerToSpawn);
                    else if (Dragon_Tower != null && gameObject.GetComponent<Player>().MinusSouls(600) == true) //Dragon
                        Instantiate(TowerToSpawn);
                    else if (Skeleton_Tower != null && gameObject.GetComponent<Player>().MinusSouls(200) == true) //Skeleton
                        Instantiate(TowerToSpawn);
                    else if (Zombie_Tower != null && gameObject.GetComponent<Player>().MinusSouls(550) == true) //Zombie
                        Instantiate(TowerToSpawn);
                    else if (Demon_Girl_Tower != null && gameObject.GetComponent<Player>().MinusSouls(300) == true) //Succubus
                        Instantiate(TowerToSpawn);
                    else if (Archer_Tower != null && gameObject.GetComponent<Player>().MinusSouls(450) == true) //Archer
                        Instantiate(TowerToSpawn);
                    else if (Ground_Dragon_Tower != null && gameObject.GetComponent<Player>().MinusSouls(750) == true) //Ground Dragon
                        Instantiate(TowerToSpawn);
                    else if (Soul_Grinder_Tower != null && gameObject.GetComponent<Player>().MinusSouls(1000) == true) //Money Maker
                        Instantiate(TowerToSpawn);

                    Particlewhenplaced.transform.position = TowerToSpawn.transform.position;
                    Instantiate(Particlewhenplaced);
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
                    TowerToSpawn = null;
                }

            }
        }
        else if (TowerToSpawn == null) //If there are no towers to place, let player interact again
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

    private void Player_Attack()
    {
        //If player presses the left mouse button and isn't already attacking
        if (Input.GetKeyDown(KeyCode.Mouse0) && Attack == false)
        {
            if (!gameObject.GetComponent<AudioSource>().isPlaying) //If Attack SFX isn't playing
            {
                gameObject.GetComponent<AudioSource>().Play(); //Play it
            }

            //Then activate the sword and enable it's script to have it do damage
            PlayerSword.SetActive(true);
            PlayerSword.GetComponent<AttackScript>().enabled = true;
            Attack = true; //Set bool to true to prevent another mouse click until attack is finished
        }

        //When attack is true
        if(Attack == true)
        {
            //Rotate the sword around the player in front of them
            PlayerSword.transform.RotateAround(PlayerCharacter.transform.position, Vector3.up, -360 * Time.deltaTime);

            //Timer to increase
            AttackTime += 1f * Time.deltaTime;

            //When Timer reaches a certain amount
            if (AttackTime >= 0.495f)
            {
                //Reset it then disable the sword and let player press left mouse button again to attack once more
                AttackTime = 0;
                Attack = false;

                DisableSword();
            }
        }
    }

    public bool GetUpgradeBool()
    {
        return upgrade;
    }

    public void SetUpgradeBool(bool newUpgrade)
    {
        upgrade = newUpgrade;
    }
}