using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/LeechBlood")]
public class LeechBlood : PowerupEffect
{
    public override void Apply(GameObject target)
    {
        if (!target.CompareTag("Player"))
        {
            return;
        }
        else
        {
            target.GetComponent<PlayerMovement>().leech = true;
        }
    }
}
