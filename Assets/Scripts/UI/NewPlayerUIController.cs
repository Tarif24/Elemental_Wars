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

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        if (!playerSave.isSaveGame)
        {
            Time.timeScale = 0;
            newPlayer.SetActive(true);
            player.isInNewPlayer = true;
        }
    }

    public void Enter()
    {
        player.Name = nameInput.text;

        int element = elementDropDown.value;

        switch (element) 
        {
            case 0:
                player.type = ElementalMonsterType.Fire;
                break;

            case 1:
                player.type = ElementalMonsterType.Air;
                break;

            case 2:
                player.type = ElementalMonsterType.Water;
                break;

            case 3:
                player.type = ElementalMonsterType.Earth;
                break;

            case 4:
                player.type = ElementalMonsterType.Electric;
                break;

            default:
                player.type = ElementalMonsterType.None;
                break;
        }
        player.isInNewPlayer = false;
        Time.timeScale = 1.0f;
        newPlayer.SetActive(false);
    }
}
