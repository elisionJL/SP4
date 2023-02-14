using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_Shop : MonoBehaviour
{
    public GameObject Player;

    // Update is called once per frame
    void Update()
    {
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
    }

    public void Tower2Select()
    {
        //If user has previously selected a tower
        if (Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn != null)
            Destroy(Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn);

        Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn = Player.gameObject.GetComponent<Base_Interaction>().Towers[1];

        Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn = Instantiate(Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn);
    }

    public void Tower3Select()
    {
        //If user has previously selected a tower
        if (Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn != null)
            Destroy(Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn);

        Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn = Player.gameObject.GetComponent<Base_Interaction>().Towers[2];
        Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn = Instantiate(Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn);
    }

    public void Tower4Select()
    {
        //If user has previously selected a tower
        if (Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn != null)
            Destroy(Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn);

        Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn = Player.gameObject.GetComponent<Base_Interaction>().Towers[3];
        Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn = Instantiate(Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn);
    }

    public void Tower5Select()
    {
        //If user has previously selected a tower
        if (Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn != null)
            Destroy(Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn);

        Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn = Player.gameObject.GetComponent<Base_Interaction>().Towers[4];
        Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn = Instantiate(Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn);
    }
}
