using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BulletsUI : MonoBehaviour
{
    private TextMeshProUGUI text;
    private PlayerMovement player;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        player = PlayerMovement.instance;
    }

    private void Update()
    {
        text.text = $"{player.currentBullets}/{player.maxBullets}";
    }
}
