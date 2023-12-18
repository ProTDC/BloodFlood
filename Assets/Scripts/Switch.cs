using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public Animator anim;
    public bool leverBool = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            anim.SetTrigger("TurnOn");
            leverBool = true;
        }
    }
}
