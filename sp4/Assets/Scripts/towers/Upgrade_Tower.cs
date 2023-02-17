using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Upgrade_Tower : MonoBehaviour
{
    public TextMeshProUGUI Name, Cost, AttackDetails, HPDetails;
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
            Cost.text = "Cost: " + Tower.gameObject.GetComponent<DragonTower>().GetUpgradeCost();
            HPDetails.text = "HP: " + Tower.gameObject.GetComponent<DragonTower>().GetHP() + "  ->  <color=green>" + Tower.gameObject.GetComponent<DragonTower>().GetHPUpgraded() + "</color>";
            AttackDetails.text = "Atk: " + Tower.gameObject.GetComponent<DragonTower>().GetDamage() + "  ->  <color=green>" + Tower.gameObject.GetComponent<DragonTower>().GetDamageUpgraded() + "</color>";

            Debug.Log(Tower.gameObject.GetComponent<DragonTower>().GetHP());
            Debug.Log(Tower.gameObject.GetComponent<DragonTower>().GetDamage());
        }

        else if (Tower.gameObject.GetComponent<MageTower>() != null)
        {
            Name.text = "" + Tower.gameObject.GetComponent<MageTower>().GetName();
            Cost.text = "Cost: " + Tower.gameObject.GetComponent<MageTower>().GetUpgradeCost();
            HPDetails.text = "HP: " + Tower.gameObject.GetComponent<MageTower>().GetHP() + "  ->  <color=green>" + Tower.gameObject.GetComponent<MageTower>().GetHPUpgraded() + "</color>";
            AttackDetails.text = "Atk: " + Tower.gameObject.GetComponent<MageTower>().GetDamage() + "  ->  <color=green>" + Tower.gameObject.GetComponent<MageTower>().GetDamageUpgraded() + "</color>";
        }

        else if (Tower.gameObject.GetComponent<SkeletonTower>() != null)
        {
            Name.text = "" + Tower.gameObject.GetComponent<SkeletonTower>().GetName();
            Cost.text = "Cost: " + Tower.gameObject.GetComponent<SkeletonTower>().GetUpgradeCost();
            HPDetails.text = "HP: " + Tower.gameObject.GetComponent<SkeletonTower>().GetHP() + "  ->  <color=green>" + Tower.gameObject.GetComponent<SkeletonTower>().GetHPUpgraded() + "</color>";
            AttackDetails.text = "Atk: " + Tower.gameObject.GetComponent<SkeletonTower>().GetDamage() + "  ->  <color=green>" + Tower.gameObject.GetComponent<SkeletonTower>().GetDamageUpgraded() + "</color>";
        }

        else if (Tower.gameObject.GetComponent<DemonGirlTower>() != null)
        {
            Name.text = "" + Tower.gameObject.GetComponent<DemonGirlTower>().GetName();
            Cost.text = "Cost: " + Tower.gameObject.GetComponent<DemonGirlTower>().GetUpgradeCost();
            HPDetails.text = "HP: " + Tower.gameObject.GetComponent<DemonGirlTower>().GetHP() + "  ->  <color=green>" + Tower.gameObject.GetComponent<DemonGirlTower>().GetHPUpgraded() + "</color>";
            AttackDetails.text = "Atk: " + Tower.gameObject.GetComponent<DemonGirlTower>().GetDamage() + "  ->  <color=green>" + Tower.gameObject.GetComponent<DemonGirlTower>().GetDamageUpgraded() + "</color>";
        }

        else if (Tower.gameObject.GetComponent<ZombieTower>() != null)
        {
            Name.text = "" + Tower.gameObject.GetComponent<ZombieTower>().GetName();
            Cost.text = "Cost: " + Tower.gameObject.GetComponent<ZombieTower>().GetUpgradeCost();
            HPDetails.text = "HP: " + Tower.gameObject.GetComponent<ZombieTower>().GetHP() + "  ->  <color=green>" + Tower.gameObject.GetComponent<ZombieTower>().GetHPUpgraded() + "</color>";
            AttackDetails.text = "Atk: " + Tower.gameObject.GetComponent<ZombieTower>().GetDamage() + "  ->  <color=green>" + Tower.gameObject.GetComponent<ZombieTower>().GetDamageUpgraded() + "</color>";
        }

        else if (Tower.gameObject.GetComponent<ArcherTower>() != null)
        {
            Name.text = "" + Tower.gameObject.GetComponent<ArcherTower>().GetName();
            Cost.text = "Cost: " + Tower.gameObject.GetComponent<ArcherTower>().GetUpgradeCost();
            HPDetails.text = "HP: " + Tower.gameObject.GetComponent<ArcherTower>().GetHP() + "  ->  <color=green>" + Tower.gameObject.GetComponent<ArcherTower>().GetHPUpgraded() + "</color>";
            AttackDetails.text = "Atk: " + Tower.gameObject.GetComponent<ArcherTower>().GetDamage() + "  ->  <color=green>" + Tower.gameObject.GetComponent<ArcherTower>().GetDamageUpgraded() + "</color>";
        }

        else if (Tower.gameObject.GetComponent<GroundDragonTower>() != null)
        {
            Name.text = "" + Tower.gameObject.GetComponent<GroundDragonTower>().GetName();
            Cost.text = "Cost: " + Tower.gameObject.GetComponent<GroundDragonTower>().GetUpgradeCost();
            HPDetails.text = "HP: " + Tower.gameObject.GetComponent<GroundDragonTower>().GetHP() + "  ->  <color=green>" + Tower.gameObject.GetComponent<GroundDragonTower>().GetHPUpgraded() + "</color>";
            AttackDetails.text = "Atk: " + Tower.gameObject.GetComponent<GroundDragonTower>().GetDamage() + "  ->  <color=green>" + Tower.gameObject.GetComponent<GroundDragonTower>().GetDamageUpgraded() + "</color>";
        }

        else if (Tower.gameObject.GetComponent<SoulGrinderTower>() != null)
        {
            Name.text = "" + Tower.gameObject.GetComponent<SoulGrinderTower>().GetName();
            Cost.text = "Cost: " + Tower.gameObject.GetComponent<SoulGrinderTower>().GetUpgradeCost();
            AttackDetails.text = "Souls Generated: " + Tower.gameObject.GetComponent<SoulGrinderTower>().GetSoulsGeneration() + "  ->  <color=green>" + Tower.gameObject.GetComponent<SoulGrinderTower>().GetSoulsUpgraded() + "</color>";
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
        if (TowerGotten.gameObject.GetComponent<DragonTower>() != null &&
            Player.gameObject.GetComponent<Player>().MinusSouls(TowerGotten.gameObject.GetComponent<DragonTower>().GetUpgradeCost()) == true)
            TowerGotten.gameObject.GetComponent<DragonTower>().UpgradeStats();
        else if (TowerGotten.gameObject.GetComponent<MageTower>() != null &&
            Player.gameObject.GetComponent<Player>().MinusSouls(TowerGotten.gameObject.GetComponent<MageTower>().GetUpgradeCost()) == true)
            TowerGotten.gameObject.GetComponent<MageTower>().UpgradeStats();
        else if (TowerGotten.gameObject.GetComponent<SkeletonTower>() != null &&
            Player.gameObject.GetComponent<Player>().MinusSouls(TowerGotten.gameObject.GetComponent<SkeletonTower>().GetUpgradeCost()) == true)
            TowerGotten.gameObject.GetComponent<SkeletonTower>().UpgradeStats();
        else if (TowerGotten.gameObject.GetComponent<DemonGirlTower>() != null &&
            Player.gameObject.GetComponent<Player>().MinusSouls(TowerGotten.gameObject.GetComponent<DemonGirlTower>().GetUpgradeCost()) == true)
            TowerGotten.gameObject.GetComponent<DemonGirlTower>().UpgradeStats();
        else if (TowerGotten.gameObject.GetComponent<ZombieTower>() != null &&
            Player.gameObject.GetComponent<Player>().MinusSouls(TowerGotten.gameObject.GetComponent<ZombieTower>().GetUpgradeCost()) == true)
            TowerGotten.gameObject.GetComponent<ZombieTower>().UpgradeStats();
        else if (TowerGotten.gameObject.GetComponent<ArcherTower>() != null &&
            Player.gameObject.GetComponent<Player>().MinusSouls(TowerGotten.gameObject.GetComponent<ArcherTower>().GetUpgradeCost()) == true)
            TowerGotten.gameObject.GetComponent<ArcherTower>().UpgradeStats();
        else if (TowerGotten.gameObject.GetComponent<GroundDragonTower>() != null &&
            Player.gameObject.GetComponent<Player>().MinusSouls(TowerGotten.gameObject.GetComponent<GroundDragonTower>().GetUpgradeCost()) == true)
            TowerGotten.gameObject.GetComponent<GroundDragonTower>().UpgradeStats();
        else if (TowerGotten.gameObject.GetComponent<SoulGrinderTower>() != null &&
            Player.gameObject.GetComponent<Player>().MinusSouls(TowerGotten.gameObject.GetComponent<SoulGrinderTower>().GetUpgradeCost()) == true)
            TowerGotten.gameObject.GetComponent<SoulGrinderTower>().UpgradeSouldGrinder();

        gameObject.SetActive(false);
        Player.gameObject.GetComponent<Player>().LockMouse();
        Player.gameObject.GetComponent<Base_Interaction>().CloseShopUI();
    }
}
