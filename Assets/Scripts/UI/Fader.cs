using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    //public Text regionName;

    //public float fadeTime;
    //public bool fadingIn;
    //public bool fadingDone;

    //void Start()
    //{
    //    regionName.CrossFadeAlpha(0, 0.0f, false);
    //    fadeTime = 0;
    //    fadingIn = false;
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if (fadingIn)
    //    {
    //        FadeIn();
    //    }
    //    else if (regionName.color.a != 0)
    //    {
    //        regionName.CrossFadeAlpha(0, 0.5f, false);
    //    }
    //}

    //void FadeIn()
    //{
    //    regionName.CrossFadeAlpha(1, 0.5f, false);
    //    fadeTime += Time.deltaTime;

    //    if (regionName.color.a == 1 && fadeTime > 1.5f)
    //    {
    //        fadingIn = false;
    //        fadeTime = 0;
    //    }
    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Region"))
    //    {
    //        fadingIn = true;
    //        regionName.text = collision.name;
    //    }
    //}
}
