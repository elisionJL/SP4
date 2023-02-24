using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tower_Shop : MonoBehaviour
{
    public GameObject Player;
    public GameObject Cost;
    public TextMeshProUGUI CostUI;
    public float Cost_Cooldown;
    public float Delay;
    public bool isDelayed = false;
    public bool isDisabled = false;

    private void Start()
    {
        Cost = GameObject.Find("_Inventory_");
        CostUI = Cost.transform.GetChild(10).GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDelayed == true || isDisabled == true)
        {
            Delay -= 1 * Time.deltaTime;
        }
        if(Delay <= 0 && isDelayed == true)
        {
            CostUI.text = "Cost: " + GetTowerComponent(Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn);
            isDelayed = false;
            isDisabled = true;
            Delay = 0.1f;
        }
        else if(Delay <= 0 && isDisabled == true)
        {
            DisableTower(Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn);
            isDisabled = false;
        }
        if (Cost_Cooldown > 0)
            Cost_Cooldown -= 1 * Time.deltaTime;
        else if (Cost_Cooldown <= 0)
            CostUI.text = "";
        if (Input.GetKeyDown(KeyCode.Alpha1))
            Tower1Select();
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            Tower2Select();
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            Tower3Select();
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            Tower4Select();
        else if (Input.GetKeyDown(KeyCode.Alpha5))
            Tower5Select();
    }

    public void DisableTower(GameObject TowerGet)
    {
        if (TowerGet.GetComponent<MageTower>() != null) //Mage
            TowerGet.GetComponent<MageTower>().enabled = false;
        else if (TowerGet.GetComponent<DragonTower>() != null) //Dragon
            TowerGet.GetComponent<DragonTower>().enabled = false;
        else if (TowerGet.GetComponent<SkeletonTower>() != null) //Skeleton
            TowerGet.GetComponent<SkeletonTower>().enabled = false;
        else if (TowerGet.GetComponent<DemonGirlTower>() != null) //Succubus
            TowerGet.GetComponent<DemonGirlTower>().enabled = false;
        else if (TowerGet.GetComponent<ZombieTower>() != null) //Zombie
            TowerGet.GetComponent<ZombieTower>().enabled = false;
        else if (TowerGet.GetComponent<ArcherTower>() != null) //Archer
            TowerGet.GetComponent<ArcherTower>().enabled = false;
        else if (TowerGet.GetComponent<GroundDragonTower>() != null) //Ground Dragon
            TowerGet.GetComponent<GroundDragonTower>().enabled = false;
        else if (TowerGet.GetComponent<SoulGrinderTower>() != null) //Money Maker
            TowerGet.GetComponent<SoulGrinderTower>().enabled = false;
    }

    public void EnableTower(GameObject TowerGet)
    {
        if (TowerGet.GetComponent<MageTower>() != null) //Mage
            TowerGet.GetComponent<MageTower>().enabled = true;
        else if (TowerGet.GetComponent<DragonTower>() != null) //Dragon
            TowerGet.GetComponent<DragonTower>().enabled = true;
        else if (TowerGet.GetComponent<SkeletonTower>() != null) //Skeleton
            TowerGet.GetComponent<SkeletonTower>().enabled = true;
        else if (TowerGet.GetComponent<DemonGirlTower>() != null) //Succubus
            TowerGet.GetComponent<DemonGirlTower>().enabled = true;
        else if (TowerGet.GetComponent<ZombieTower>() != null) //Zombie
            TowerGet.GetComponent<ZombieTower>().enabled = true;
        else if (TowerGet.GetComponent<ArcherTower>() != null) //Archer
            TowerGet.GetComponent<ArcherTower>().enabled = true;
        else if (TowerGet.GetComponent<GroundDragonTower>() != null) //Ground Dragon
            TowerGet.GetComponent<GroundDragonTower>().enabled = true;
        else if (TowerGet.GetComponent<SoulGrinderTower>() != null) //Money Maker
            TowerGet.GetComponent<SoulGrinderTower>().enabled = true;
    }
    public int GetTowerComponent(GameObject TowerGet)
    {
        if (TowerGet.GetComponent<MageTower>() != null) //Mage
            return TowerGet.GetComponent<MageTower>().GetCost();
        else if (TowerGet.GetComponent<DragonTower>() != null) //Dragon
            return TowerGet.GetComponent<DragonTower>().GetCost();
        else if (TowerGet.GetComponent<SkeletonTower>() != null) //Skeleton
            return TowerGet.GetComponent<SkeletonTower>().GetCost();
        else if (TowerGet.GetComponent<DemonGirlTower>() != null) //Succubus
            return TowerGet.GetComponent<DemonGirlTower>().GetCost();
        else if (TowerGet.GetComponent<ZombieTower>() != null) //Zombie
            return TowerGet.GetComponent<ZombieTower>().GetCost();
        else if (TowerGet.GetComponent<ArcherTower>() != null) //Archer
            return TowerGet.GetComponent<ArcherTower>().GetCost();
        else if (TowerGet.GetComponent<GroundDragonTower>() != null) //Ground Dragon
            return TowerGet.GetComponent<GroundDragonTower>().GetCost();
        else if (TowerGet.GetComponent<SoulGrinderTower>() != null) //Money Maker
            return TowerGet.GetComponent<SoulGrinderTower>().GetCost();

        return 1000;
    }
    //Dragon Tower
    public void Tower1Select()
    {        
        //If user has previously selected a tower
        if (Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn != null)
            Destroy(Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn); //Destroy previous tower to prevent ghost towers from being placed

        //Set the tower user wants to spawn from the tower list array that contains all the towers to the dragon tower
        Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn = Player.gameObject.GetComponent<Base_Interaction>().Towers[0];
        //Instantiate the tower and get its reference
        Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn = Instantiate(Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn);

        EnableTower(Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn);
        isDelayed = true;
        Delay = 0.1f;

        Cost_Cooldown = 1.5f;
    }

    public void Tower2Select()
    {
        //If user has previously selected a tower
        if (Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn != null)
            Destroy(Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn);

        Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn = Player.gameObject.GetComponent<Base_Interaction>().Towers[1];
        Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn = Instantiate(Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn);

        EnableTower(Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn);
        isDelayed = true;
        Delay = 0.1f;

        Cost_Cooldown = 1.5f;
    }

    public void Tower3Select()
    {
        //If user has previously selected a tower
        if (Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn != null)
            Destroy(Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn);

        Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn = Player.gameObject.GetComponent<Base_Interaction>().Towers[2];
        Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn = Instantiate(Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn);

        EnableTower(Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn);
        isDelayed = true;
        Delay = 0.1f;

        Cost_Cooldown = 1.5f;
    }

    public void Tower4Select()
    {
        //If user has previously selected a tower
        if (Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn != null)
            Destroy(Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn);

        Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn = Player.gameObject.GetComponent<Base_Interaction>().Towers[3];
        Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn = Instantiate(Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn);

        EnableTower(Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn);
        isDelayed = true;
        Delay = 0.1f;

        Cost_Cooldown = 1.5f;
    }

    public void Tower5Select()
    {
        //If user has previously selected a tower
        if (Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn != null)
            Destroy(Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn);

        Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn = Player.gameObject.GetComponent<Base_Interaction>().Towers[4];
        Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn = Instantiate(Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn);

        EnableTower(Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn);
        isDelayed = true;
        Delay = 0.1f;

        Cost_Cooldown = 1.5f;
    }
}
