using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharpThing : MonoBehaviour
{
    public int sharpThingDamage;
    public int sharpThingDamageEnraged;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement player = collision.GetComponent<PlayerMovement>();

        if (player != null)
        {
            player.Damage(sharpThingDamage);
        }
    }
}
