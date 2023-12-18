using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue_Test : MonoBehaviour
{
    public GameObject button;
    public GameObject player;
    public GameObject dialogue;
    public bool W_KeyOn;
    public bool dialogueActive;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            button.SetActive(true);
            W_KeyOn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            button.SetActive(false);
            W_KeyOn = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && W_KeyOn == true)
        {
            player.GetComponent<PlayerMovement>().enabled = false;
            dialogue.SetActive(true);
            dialogueActive = true;
        }

        else if (Input.GetKeyDown(KeyCode.Escape) && dialogueActive == true || dialogue.activeSelf == false)
        {
            player.GetComponent<PlayerMovement>().enabled = true;
            dialogueActive = false;
        }

    }
}
