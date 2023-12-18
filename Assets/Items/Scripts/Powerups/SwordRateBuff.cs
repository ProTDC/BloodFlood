using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/SwordRateBuff")]

public class SwordRateBuff : PowerupEffect
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
            target.GetComponent<MeleeAttackManager>().cooldownTime += amount;
        }
    }
}
