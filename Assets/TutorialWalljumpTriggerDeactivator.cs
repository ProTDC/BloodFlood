using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialWalljumpTriggerDeactivator : MonoBehaviour
{
    public GameObject wallJumpPopup;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            wallJumpPopup.SetActive(false);
            Destroy(this);
        }
    }
}
