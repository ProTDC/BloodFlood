using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Terminal_Menu_Logic : MonoBehaviour
{
    public Button save;
    public Button skills;
    public Button quit;

    public Image[] arrowImg;

    public int selected = 0;

    public GameObject skillTree;
    public PlayerMovement player;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    public void EnablePlayer()
    {
        player.speed = 10;
    }

    public void LoadMain()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
