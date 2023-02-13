using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_Shop : MonoBehaviour
{
    public GameObject Player;

    public void Tower1Select()
    {
        if (Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn != null)
            Destroy(Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn);

        Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn = Player.gameObject.GetComponent<Base_Interaction>().Towers[0].gameObject;
        Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn = Instantiate(Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn);
        Player.gameObject.GetComponent<Player>().ShopUI.SetActive(false);
    }

    public void Tower2Select()
    {
        if (Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn != null)
            Destroy(Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn);

        Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn = Player.gameObject.GetComponent<Base_Interaction>().Towers[1];
        Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn = Instantiate(Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn);
        Player.gameObject.GetComponent<Player>().ShopUI.SetActive(false);
    }

    public void Tower3Select()
    {
        if (Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn != null)
            Destroy(Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn);

        Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn = Player.gameObject.GetComponent<Base_Interaction>().Towers[2];
        Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn = Instantiate(Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn);
        Player.gameObject.GetComponent<Player>().ShopUI.SetActive(false);
    }

    public void Tower4Select()
    {
        if (Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn != null)
            Destroy(Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn);

        Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn = Player.gameObject.GetComponent<Base_Interaction>().Towers[3];
        Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn = Instantiate(Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn);
        Player.gameObject.GetComponent<Player>().ShopUI.SetActive(false);
    }

    public void Tower5Select()
    {
        if (Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn != null)
            Destroy(Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn);

        Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn = Player.gameObject.GetComponent<Base_Interaction>().Towers[4];
        Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn = Instantiate(Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn);
        Player.gameObject.GetComponent<Player>().ShopUI.SetActive(false);
    }
}
