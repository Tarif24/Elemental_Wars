using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{
    public SO_PlayerSaveData saveData;

    public GameObject menu;
    public GameObject credits;

    public void NewGame()
    {
        saveData.isSaveGame = false;
        SceneManager.LoadScene(1);
    }

    public void SaveGame()
    {
        saveData.isSaveGame = true;
        SceneManager.LoadScene(1);
    }

    public void Credits()
    {
        menu.SetActive(false);
        credits.SetActive(true);
    }

    public void MainMenu()
    {
        menu.SetActive(true);
        credits.SetActive(false);
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
