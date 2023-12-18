using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Fishing/Fish")]

public class AddFishToEntry : PowerupEffect
{
    public Fishstiary fish;
    public int id;
    public bool isActived;

    public override void Apply(GameObject target)
    {
        if (!target.CompareTag("Player"))
        {
            return;
        }
        else
        {
            fish = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Fishstiary>();
            fish.EnableEntryButton(id);
            isActived = true;
        }

    }
}
