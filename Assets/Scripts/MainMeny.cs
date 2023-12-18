using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMeny : MonoBehaviour
{
    public LevelLoader LevelLoader;

    public void PlayGame()
    {
        LevelLoader.LoadNextLevel();
    }

    public void PlayFishing()
    {
        LevelLoader.LoadFishingLevel();
    }

    public void PlayPlanet()
    {
        LevelLoader.LoadPlanetLevel();
    }

    public void PlaySingle(int id)
    {
        LevelLoader.LoadSingleLevel(id);
    }

    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
