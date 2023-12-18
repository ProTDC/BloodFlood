using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Go_To_Next_Scene : MonoBehaviour
{
    public float cutsceneTime = 14.31f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(NextScene());
    }

    IEnumerator NextScene()
    {
        yield return new WaitForSeconds(cutsceneTime);
        SceneManager.LoadScene("Game");
    }
}
