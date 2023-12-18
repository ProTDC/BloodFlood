using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Powerups/GunRateBuff")]

public class GunRateBuff : PowerupEffect
{
    public float amount;

    public override void Apply(GameObject target)
    {
        if (!target.CompareTag("Player"))
        {
            return;
        }
        else
        {
            target.GetComponent<Weapon>().shootingCooldown -= amount;
        }
    }
}
