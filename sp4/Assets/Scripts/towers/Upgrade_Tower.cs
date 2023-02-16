using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Upgrade_Tower : MonoBehaviour
{
    public TextMeshProUGUI Name, Cost;
    public GameObject Player;

    private GameObject TowerGotten;

    private void OnEnable()
    {
        
    }

    public void GetTowerInfo(GameObject Tower)
    {
        if (Tower.gameObject.GetComponent<DragonTower>() != null)
        {
            Debug.Log(Tower.gameObject.GetComponent<DragonTower>().GetName());
            Name.text = "" + Tower.gameObject.GetComponent<DragonTower>().GetName();
            Cost.text = "" + Tower.gameObject.GetComponent<DragonTower>().GetCost();
        }

        else if (Tower.gameObject.GetComponent<MageTower>() != null)
        {
            Name.text = "" + Tower.gameObject.GetComponent<MageTower>().GetName();
            Cost.text = "" + Tower.gameObject.GetComponent<MageTower>().GetCost();
        }

        else if (Tower.gameObject.GetComponent<SkeletonTower>() != null)
        {
            Name.text = "" + Tower.gameObject.GetComponent<SkeletonTower>().GetName();
            Cost.text = "" + Tower.gameObject.GetComponent<SkeletonTower>().GetCost();
        }

        else if (Tower.gameObject.GetComponent<DemonGirlTower>() != null)
        {
            Name.text = "" + Tower.gameObject.GetComponent<DemonGirlTower>().GetName();
            Cost.text = "" + Tower.gameObject.GetComponent<DemonGirlTower>().GetCost();
        }

        else if (Tower.gameObject.GetComponent<ZombieTower>() != null)
        {
            Name.text = "" + Tower.gameObject.GetComponent<ZombieTower>().GetName();
            Cost.text = "" + Tower.gameObject.GetComponent<ZombieTower>().GetCost();
        }

        else if (Tower.gameObject.GetComponent<ArcherTower>() != null)
        {
            Name.text = "" + Tower.gameObject.GetComponent<ArcherTower>().GetName();
            Cost.text = "" + Tower.gameObject.GetComponent<ArcherTower>().GetCost();
        }

        else if (Tower.gameObject.GetComponent<GroundDragonTower>() != null)
        {
            Name.text = "" + Tower.gameObject.GetComponent<GroundDragonTower>().GetName();
            Cost.text = "" + Tower.gameObject.GetComponent<GroundDragonTower>().GetCost();
        }

        else if (Tower.gameObject.GetComponent<SoulGrinderTower>() != null)
        {
            Name.text = "" + Tower.gameObject.GetComponent<SoulGrinderTower>().GetName();
            Cost.text = "" + Tower.gameObject.GetComponent<SoulGrinderTower>().GetCost();
        }

        TowerGotten = Tower;
    }

    public void CloseUI()
    {
        gameObject.SetActive(false);
        Player.gameObject.GetComponent<Player>().LockMouse();
        Player.gameObject.GetComponent<Base_Interaction>().CloseShopUI();
    }

    public void SellTower() //Give back some souls to destroy tower
    {
        Destroy(TowerGotten);
        gameObject.SetActive(false);
        Player.gameObject.GetComponent<Player>().LockMouse();
        Player.gameObject.GetComponent<Base_Interaction>().CloseShopUI();
    }

    public void UpgradeTower()
    {
        if (TowerGotten.gameObject.GetComponent<DragonTower>() != null)
            Player.gameObject.GetComponent<Player>().MinusSouls(TowerGotten.gameObject.GetComponent<DragonTower>().GetUpgradeCost());
        else if (TowerGotten.gameObject.GetComponent<MageTower>() != null)
            Player.gameObject.GetComponent<Player>().MinusSouls(TowerGotten.gameObject.GetComponent<MageTower>().GetUpgradeCost());
        else if (TowerGotten.gameObject.GetComponent<SkeletonTower>() != null)
            Player.gameObject.GetComponent<Player>().MinusSouls(TowerGotten.gameObject.GetComponent<SkeletonTower>().GetUpgradeCost());
        else if (TowerGotten.gameObject.GetComponent<DemonGirlTower>() != null)
            Player.gameObject.GetComponent<Player>().MinusSouls(TowerGotten.gameObject.GetComponent<DemonGirlTower>().GetUpgradeCost());
        else if (TowerGotten.gameObject.GetComponent<ZombieTower>() != null)
            Player.gameObject.GetComponent<Player>().MinusSouls(TowerGotten.gameObject.GetComponent<ZombieTower>().GetUpgradeCost());
        else if (TowerGotten.gameObject.GetComponent<ArcherTower>() != null)
            Player.gameObject.GetComponent<Player>().MinusSouls(TowerGotten.gameObject.GetComponent<ArcherTower>().GetUpgradeCost());
        else if (TowerGotten.gameObject.GetComponent<GroundDragonTower>() != null)
            Player.gameObject.GetComponent<Player>().MinusSouls(TowerGotten.gameObject.GetComponent<GroundDragonTower>().GetUpgradeCost());
        else if (TowerGotten.gameObject.GetComponent<SoulGrinderTower>() != null)
            Player.gameObject.GetComponent<Player>().MinusSouls(TowerGotten.gameObject.GetComponent<SoulGrinderTower>().GetUpgradeCost());

        Debug.Log("Upgraded!"); //To be changed when we finally get stat upgrades decided

        gameObject.SetActive(false);
        Player.gameObject.GetComponent<Player>().LockMouse();
        Player.gameObject.GetComponent<Base_Interaction>().CloseShopUI();
    }
}
