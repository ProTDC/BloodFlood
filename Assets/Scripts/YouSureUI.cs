using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class YouSureUI : MonoBehaviour
{
    public GameObject skillsMenu;
    public Button yes;
    public Button no;
    public Button ok;

    public Image[] arrowImg;
    public Text upgradeText;

    public int selected = 0;
    public bool canBuy = false;

    public PlayerMovement player;

    public static YouSureUI instance;

    private void Start()
    {
        instance = this;
    }

    void Update()
    {
        if (selected >= 2)
        {
            selected = 1;
        }
        if (selected <= 0)
        {
            selected = 0;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            selected += 1;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            selected -= 1;
        }

        switch (selected)
        {
            case 0:
                arrowImg[0].gameObject.SetActive(true);
                arrowImg[1].gameObject.SetActive(false);
                break;

            case 1:
                arrowImg[0].gameObject.SetActive(false);
                arrowImg[1].gameObject.SetActive(true);
                break;
        }

        if (Input.GetKeyDown(KeyCode.Return) && selected == 1)
        {
            this.gameObject.SetActive(false);
            skillsMenu.gameObject.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Return) && selected == 0 && canBuy)
        {

        }

        if (Input.GetKeyDown(KeyCode.Return) && selected == 0 && canBuy == false)
        {
            upgradeText.text = "You don't have enough points for this upgrade (hah poor XD)";

            //yes.enabled = false;
            //no.enabled = false;
            //ok.enabled = true;

            //if (Input.GetKeyDown(KeyCode.Return))
            //{
            //    this.gameObject.SetActive(false);
            //    skillsMenu.gameObject.SetActive(true);

            //    yes.enabled = true;
            //    no.enabled = true;
            //    ok.enabled = false;
            //}
        }
    }
}
