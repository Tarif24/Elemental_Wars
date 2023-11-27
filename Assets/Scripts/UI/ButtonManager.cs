using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [SerializeField]
    SO_PlayerSaveData isSafePlayer;

    public void Flee()
    {
        isSafePlayer.isSafe = true;
        SceneManager.LoadScene(0);
    }
}
