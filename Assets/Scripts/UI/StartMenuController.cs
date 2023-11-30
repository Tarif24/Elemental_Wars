using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{
    public SO_PlayerSaveData saveData;

    public GameObject menu;
    public GameObject credits;

    public AudioSource buttonSound;

    public void NewGame()
    {
        saveData.isSaveGame = false;
        buttonSound.Play();
        SceneManager.LoadScene(1);
    }

    public void SaveGame()
    {
        saveData.isSaveGame = true;

        buttonSound.Play();

        if (File.Exists("PlayerSave.txt"))
        {
            SceneManager.LoadScene(1);
        }
    }

    public void Credits()
    {
        buttonSound.Play();
        menu.SetActive(false);
        credits.SetActive(true);
    }

    public void MainMenu()
    {
        buttonSound.Play();
        menu.SetActive(true);
        credits.SetActive(false);
    }

    public void Quit()
    {
        buttonSound.Play();
        Debug.Log("Quit");
        Application.Quit();
    }
}
