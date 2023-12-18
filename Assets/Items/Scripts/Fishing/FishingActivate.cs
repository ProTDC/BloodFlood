using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

[CreateAssetMenu(menuName = "Fishing/Activate")]

public class FishingActivate : PowerupEffect
{
    public override void Apply(GameObject target)
    {
        if (!target.CompareTag("Player"))
        {
            return;
        }
        else
        {
            target.transform.Find("MeleeWeapon").gameObject.SetActive(false);
            target.GetComponent<MeleeAttackManager>().enabled = false;

            target.transform.Find("FishingRod").gameObject.SetActive(true);
        }
    }
}
