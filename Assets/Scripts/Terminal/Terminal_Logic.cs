using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;

public class Terminal_Logic : MonoBehaviour
{
    public GameObject button;
    public GameObject menu;
    public Animator menuAnimator;
    public Image image;
    public GameObject player;
    public bool W_KeyOn;
    public bool menuActive;
    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

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
            audioManager.PlaySFX(audioManager.terminalTurnOn);
            menuAnimator.SetTrigger("TurnOn");
            menuAnimator.SetBool("IsOn", true);
            menu.SetActive(true);
            player.GetComponent<PlayerMovement>().speed = 0;
            menuActive = true;
        }

    }

    IEnumerator Disable()
    {
        audioManager.PlaySFX(audioManager.terminalTurnOff);
        menuAnimator.SetTrigger("TurnOff");
        menuAnimator.SetBool("IsOn", false);
        yield return new WaitForSeconds(.6f);
        menu.SetActive(false);
        player.GetComponent<PlayerMovement>().speed = 10;
        menuActive = false;
        menu.GetComponent<RectTransform>().localScale = Vector3.one;
    }

    public void DisableTerminal()
    {
        StartCoroutine(Disable());
    }
}
