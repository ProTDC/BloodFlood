using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon_Equip : MonoBehaviour
{
    public Button[] weaponButtons;

    public GameObject[] playerWeapons;

    public void EnableEntry(int id)
    {
        for (int i = 0; i < weaponButtons.Length; i++)
        {
            weaponButtons[i].interactable = true;
        }
        weaponButtons[id].interactable = false;
    }

    public void EnableWeaponEntry(int id)
    {
        for (int i = 0; i < playerWeapons.Length; i++)
        {
            playerWeapons[i].SetActive(false);
        }
        playerWeapons[id].SetActive(true);
    }
}
