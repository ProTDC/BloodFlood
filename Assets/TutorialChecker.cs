using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialChecker : MonoBehaviour
{
    [SerializeField] private Button tutorialButton;

    [SerializeField] private GameObject[] otherButtons;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("TutorialPlayed"))
        {
            if (PlayerPrefs.GetInt("TutorialPlayed") == 1)
            {
                foreach (var item in otherButtons)
                {
                    item.GetComponent<Button>().interactable = true;
                }
            }
        }
    }

    public void ActivateButtons()
    {
        PlayerPrefs.SetInt("TutorialPlayed", 1);
        PlayerPrefs.Save();
    }
}
