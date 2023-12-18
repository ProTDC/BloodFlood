using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(menuName = "Hamster/HamsterPickup")]
public class Hamster : PowerupEffect
{
    public override void Apply(GameObject target)
    {
        if (!target.CompareTag("Player"))
        {
            return;
        }
        else
        {
            target.GetComponent<HamsterHandler>().hamstersOnHand++;
            target.GetComponent<HamsterHandler>().AddToHamsters();
        }
    }
}
