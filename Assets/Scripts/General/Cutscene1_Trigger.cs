using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscene1_Trigger : MonoBehaviour
{
    public GameObject boss1;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //SceneManager.LoadScene("Cutscene1");
            boss1.SetActive(true);
        }
    }

}
