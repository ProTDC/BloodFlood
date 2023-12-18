using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Changes/Color")]

public class ColorChange : PowerupEffect
{
    public Color color;
    public override void Apply(GameObject target)
    {
        if (!target.CompareTag("Player"))
        {
            return;
        }
        else
        {
            target.GetComponentInChildren<SpriteRenderer>().color = color;
        }
    }
}
