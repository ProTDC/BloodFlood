using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialWalljumpTrigger : MonoBehaviour
{
    public GameObject wallJumpPopup;
    public bool timed;
    public float timeToDestroy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            wallJumpPopup.SetActive(true);

            if (timed)
            {
                Invoke("DestroyPopup", timeToDestroy);
                Destroy(this);
            }
        }
    }

    private void DestroyPopup()
    {
        //wallJumpPopup.SetActive(false);
        Destroy(wallJumpPopup);
    }
}
