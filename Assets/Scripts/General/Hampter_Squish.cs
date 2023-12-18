using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hampter_Squish : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.transform.localScale = new Vector3(0.1f, 0.06f, 0.1f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        }
    }
}
