using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FirstWeaponSelectionButton : MonoBehaviour
{
    public TMP_Text weaponNameText, desctiptionText;
    public Image weaponIcon;

    private Weapon assignedWeapon;

    private void Start()
    {
        Time.timeScale = 0f;
        
        /*
        UIController.instance.firstWeaponsSelectionButtons[0].UpdateButtonDisplay(PlayerController.instance.unassignedWeapons[0]);
        UIController.instance.firstWeaponsSelectionButtons[1].UpdateButtonDisplay(PlayerController.instance.unassignedWeapons[1]);
        UIController.instance.firstWeaponsSelectionButtons[2].UpdateButtonDisplay(PlayerController.instance.unassignedWeapons[2]);
        UIController.instance.firstWeaponsSelectionButtons[3].UpdateButtonDisplay(PlayerController.instance.unassignedWeapons[3]);
        UIController.instance.firstWeaponsSelectionButtons[4].UpdateButtonDisplay(PlayerController.instance.unassignedWeapons[4]);

                            OR with more logical way
        */

        for (int i = 0; i< PlayerController.instance.unassignedWeapons.Count;i++)
        {
            UIController.instance.firstWeaponsSelectionButtons[i].UpdateButtonDisplay(PlayerController.instance.unassignedWeapons[i]);
        }
    }

    public void UpdateButtonDisplay(Weapon theWeapon)
    {
        weaponIcon.sprite = theWeapon.icon;
        weaponNameText.text = theWeapon.name;
        desctiptionText.text = theWeapon.weaponDesctiptionText;

        assignedWeapon = theWeapon;
    }
    public void SelectWeapon()
    {
        if(PlayerController.instance.assignedWeapons.Count == 0)
        {
            PlayerController.instance.AddWeapon(assignedWeapon);
        }
        UIController.instance.firstWeaponSelectionPanel.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
