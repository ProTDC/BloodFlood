using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/DamageBuff")]

public class DamageBuff : PowerupEffect
{
    public int amount;

    public override void Apply(GameObject target)
    {
        if (!target.CompareTag("Player"))
        {
            return;
        }
        else
        {
            target.GetComponentInChildren<MeleeWeapon>().damageAmount += amount;
        }
    }
}
