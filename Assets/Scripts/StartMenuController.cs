using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{
    public SO_PlayerSaveData saveData;

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
}
