using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_Shop : MonoBehaviour
{
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Tower1Select()
    {
        Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn = Player.gameObject.GetComponent<Base_Interaction>().Towers[0].gameObject;
        Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn = Instantiate(Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn);
        Player.gameObject.GetComponent<Player>().ShopUI.SetActive(false);
    }

    public void Tower2Select()
    {
        Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn = Player.gameObject.GetComponent<Base_Interaction>().Towers[1];
        Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn = Instantiate(Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn);
        Player.gameObject.GetComponent<Player>().ShopUI.SetActive(false);
    }

    public void Tower3Select()
    {
        Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn = Player.gameObject.GetComponent<Base_Interaction>().Towers[2];
        Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn = Instantiate(Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn);
        Player.gameObject.GetComponent<Player>().ShopUI.SetActive(false);
    }

    public void Tower4Select()
    {
        Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn = Player.gameObject.GetComponent<Base_Interaction>().Towers[3];
        Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn = Instantiate(Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn);
        Player.gameObject.GetComponent<Player>().ShopUI.SetActive(false);
    }

    public void Tower5Select()
    {
        Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn = Player.gameObject.GetComponent<Base_Interaction>().Towers[4];
        Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn = Instantiate(Player.gameObject.GetComponent<Base_Interaction>().TowerToSpawn);
        Player.gameObject.GetComponent<Player>().ShopUI.SetActive(false);
    }
}
