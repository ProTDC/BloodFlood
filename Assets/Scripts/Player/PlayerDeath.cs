using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class PlayerDeath : MonoBehaviour
{
    private PlayerMovement player;
    public GameObject deathScreen;
    public GameObject videoObject;
    public VideoPlayer video;
    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        player = GetComponent<PlayerMovement>();
    }
    public void Death()
    {
        StartCoroutine(Respawn());
    }

    IEnumerator Respawn()
    {
        //CameraShake.Instance.ShakeCamera(8f, .8f);
        audioManager.PlaySFX(audioManager.playerDying);
        deathScreen.SetActive(true);
        //player.enabled = false;
        var boom = Instantiate(player.Blood, transform.position, Quaternion.identity);
        //Physics2D.IgnoreLayerCollision(10, 16, true);
        Debug.Log("Physics is true");
        boom.Play();
        yield return new WaitForSeconds(1.4f);
        //Physics2D.IgnoreLayerCollision(10, 16, false);
        Debug.Log("Physics is false");
        deathScreen.SetActive(false);
        //player.enabled = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
