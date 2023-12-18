using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/GunBulletBuff")]

public class GunBulletBuff : PowerupEffect
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
            target.GetComponent<PlayerMovement>().maxBullets += amount;
        }
    }
}
