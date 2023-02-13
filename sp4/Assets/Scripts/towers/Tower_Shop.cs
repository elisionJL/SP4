using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_Shop : MonoBehaviour
{
    public GameObject Player;

    //Dragon Tower
    public void Tower1Select()
    {
        //If user has previously selected a tower
        if (Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn != null)
            Destroy(Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn); //Destroy previous tower to prevent ghost towers from being placed

        //Set the tower user wants to spawn from the tower list array that contains all the towers to the dragon tower
        Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn = Player.gameObject.GetComponent<Base_Interaction>().Towers[0].gameObject;

        //Instantiate the dragon tower and get its reference
        Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn = Instantiate(Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn);

        //Disable the shop menu UI
        Player.gameObject.GetComponent<Player>().ShopUI.SetActive(false);
    }

    public void Tower2Select()
    {
        //If user has previously selected a tower
        if (Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn != null)
            Destroy(Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn);

        Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn = Player.gameObject.GetComponent<Base_Interaction>().Towers[1];

        Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn = Instantiate(Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn);

        //Disable the shop menu UI
        Player.gameObject.GetComponent<Player>().ShopUI.SetActive(false);
    }

    public void Tower3Select()
    {
        //If user has previously selected a tower
        if (Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn != null)
            Destroy(Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn);

        Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn = Player.gameObject.GetComponent<Base_Interaction>().Towers[2];
        Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn = Instantiate(Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn);

        //Disable the shop menu UI
        Player.gameObject.GetComponent<Player>().ShopUI.SetActive(false);
    }

    public void Tower4Select()
    {
        //If user has previously selected a tower
        if (Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn != null)
            Destroy(Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn);

        Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn = Player.gameObject.GetComponent<Base_Interaction>().Towers[3];
        Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn = Instantiate(Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn);

        //Disable the shop menu UI
        Player.gameObject.GetComponent<Player>().ShopUI.SetActive(false);
    }

    public void Tower5Select()
    {
        //If user has previously selected a tower
        if (Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn != null)
            Destroy(Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn);

        Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn = Player.gameObject.GetComponent<Base_Interaction>().Towers[4];
        Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn = Instantiate(Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn);

        //Disable the shop menu UI
        Player.gameObject.GetComponent<Player>().ShopUI.SetActive(false);
    }
}
