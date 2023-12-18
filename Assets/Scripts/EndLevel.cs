using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    public Timer _timer;
    public Kills kills;
    public HamsterManager hamsters;
    public GameObject endLevelScreen;
    public TextMeshProUGUI statusText;
    public TextMeshProUGUI killsText;
    public TextMeshProUGUI hamstersText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
            player.speed = 0;
            player.enabled = false;

            CompleteLevel();
        }
    }

    private void CompleteLevel()
    {
        _timer.DisableTime();
        _timer.DisplayTime(_timer.timeRemaining);
        statusText.text = $"{SceneManager.GetActiveScene().name}: Complete!";
        killsText.text = $"Kills: {kills.killsAchived}/{kills.maxKills}";
        hamstersText.text = $"Hamsters: {hamsters.hamstersAchived}/{hamsters.maxHamsters}";
        endLevelScreen.SetActive(true);

        this.gameObject.SetActive(false);

        Debug.Log("Level Completed");
    }
}
