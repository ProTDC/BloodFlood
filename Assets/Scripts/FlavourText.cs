using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FlavourText : MonoBehaviour
{
    private GameObject mainObject;
    private TextMeshProUGUI itemText;
    private TextMeshProUGUI flavourText;
    private Animator anim;
    public string funnyText;

    private void Start()
    {
        mainObject = GameObject.Find("FlavourText");

        itemText = mainObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        flavourText = mainObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        anim = mainObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(mainObject.name);

        if (collision.CompareTag("Player"))
        {
            itemText.text = this.gameObject.name.Replace("(Clone)", "");
            anim.SetTrigger("ItemGet");
            flavourText.text = funnyText;
        }

    }
}
