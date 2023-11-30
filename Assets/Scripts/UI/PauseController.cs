using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI levelValueText;
    public TextMeshProUGUI coinValueText;
    public GameObject buttons;
    public GameObject cheats;
    public Slider levelSlider;
    public Slider coinSlider;
    public Toggle instaKillToggle;
    public Toggle allBossesToggle;

    public GameObject pause;

    PlayerController player;

    public AudioSource buttonSound;

    public AbilitiesBase instaKillAbiility;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Pause();
        }
    }

    public void Menu()
    {
        buttonSound.Play();
        Save();
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void Save()
    {
        buttonSound.Play();
        player.Save();
    }

    public void Cheats()
    {
        buttonSound.Play();
        buttons.SetActive(false);
        cheats.SetActive(true);
        titleText.text = "CHEATS";
    }

    public void Apply()
    {
        buttonSound.Play();
        buttons.SetActive(true);
        cheats.SetActive(false);
        titleText.text = "PAUSE";

        player.XP = (int)levelSlider.value * 100;
        player.elementalCoins += (int)coinSlider.value;

        if (instaKillToggle.isOn)
        {
            player.Abilities.Insert(0, instaKillAbiility);
        }

        if (allBossesToggle.isOn)
        {
            player.bossTier = 4;
        }

    }

    public void Resume()
    {
        buttonSound.Play();
        pause.SetActive(false);
        Time.timeScale = 1;
    }

    public void Pause()
    {
        buttonSound.Play();
        pause.SetActive(true);
        Time.timeScale = 0;
    }

    public void UpdateValue()
    {
        levelValueText.text = levelSlider.value.ToString();
        coinValueText.text = coinSlider.value.ToString();
    }
}
