using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletCounter : MonoBehaviour
{
    //PlayerMovement player;

    public Sprite sprite1;
    public Sprite sprite2;

    public Image[] bulletImg;
    int bullets;
    int maximumBullets;

    //void Start()
    //{
    //    player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    //}

    void Update()
    {
        maximumBullets = PlayerMovement.instance.maxBullets;
        bullets = PlayerMovement.instance.currentBullets;

        switch (bullets)
        {
            case 3:
                foreach(Image img in bulletImg)
                {
                    img.sprite = sprite1;
                }
                break;

            case 2:
                bulletImg[0].sprite = sprite1;
                bulletImg[1].sprite = sprite1;
                bulletImg[2].sprite = sprite2;
                break;

            case 1:
                bulletImg[0].sprite = sprite1;
                bulletImg[1].sprite = sprite2;
                bulletImg[2].sprite = sprite2;
                break;

            case 0:
                foreach (Image img in bulletImg)
                {
                    img.sprite = sprite2;
                }
                break;
        }
    }
}
