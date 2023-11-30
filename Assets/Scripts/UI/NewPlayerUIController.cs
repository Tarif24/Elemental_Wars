using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NewPlayerUIController : MonoBehaviour
{
    public TMP_InputField nameInput;
    public TMP_Dropdown elementDropDown;
    public GameObject newPlayer;

    public SO_PlayerSaveData playerSave;

    PlayerController player;

    public AudioSource buttonSound;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        if (!playerSave.isSaveGame)
        {
            Time.timeScale = 0;
            newPlayer.SetActive(true);
            player.isBusy = true;
        }
    }

    public void Enter()
    {
        buttonSound.Play();
        player.Name = nameInput.text;

        int element = elementDropDown.value;

        switch (element) 
        {
            case 0:
                player.ChangeType(ElementalMonsterType.Fire);
                break;

            case 1:
                player.ChangeType(ElementalMonsterType.Air);
                break;

            case 2:
                player.ChangeType(ElementalMonsterType.Water);
                break;

            case 3:
                player.ChangeType(ElementalMonsterType.Earth);
                break;

            case 4:
                player.ChangeType(ElementalMonsterType.Electric);
                break;

            default:
                player.ChangeType(ElementalMonsterType.None);
                break;
        }
        player.isBusy = false;
        Time.timeScale = 1.0f;
        newPlayer.SetActive(false);
    }
}
