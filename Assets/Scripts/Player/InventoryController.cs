using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    PlayerController player;

    public TextMeshProUGUI titleText;
    public TextMeshProUGUI healthPotionText;
    public GameObject inventory;
    public GameObject inventoryItems;
    public GameObject stats;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI attackText;
    public TextMeshProUGUI defenseText;
    public TextMeshProUGUI typeText;
    public TextMeshProUGUI strengthText;
    public TextMeshProUGUI weaknessText;

    public AudioSource buttonSound;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && player.inHomeRange)
        {
            OpenInventory();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseInventory();
        }

        healthPotionText.text = "Health Potions - " + player.healthPotions;
    }

    public void OpenInventory()
    {
        buttonSound.Play();
        inventory.SetActive(true);
        player.isBusy = true;
        Time.timeScale = 0;
    }

    public void CloseInventory()
    {
        buttonSound.Play();
        inventory.SetActive(false);
        player.isBusy = false;
        Time.timeScale = 1;
    }

    public void HealthPotions()
    {
        buttonSound.Play();
        if (player.healthPotions > 0 && player.HP < player.MaxHP)
        {
            player.HP = player.MaxHP;
            player.healthPotions--;
        }
    }

    public void Stats()
    {
        buttonSound.Play();
        inventoryItems.SetActive(false);
        stats.SetActive(true);

        titleText.text = "Stats";

        nameText.text = "Name - " + player.Name;
        levelText.text = "Level - " + player.Level;
        healthText.text = "Health - " + player.HP + "/" + player.MaxHP;
        attackText.text = "Attack - " + player.Attack;
        defenseText.text = "Defense - " + player.Defense;
        typeText.text = "Type - " + player.type.ToString();
        strengthText.text = "Strength - " + player.strength.ToString();
        weaknessText.text = "Weakness - " + player.weakness.ToString();
    }

    public void Back()
    {
        buttonSound.Play();
        inventoryItems.SetActive(true);
        stats.SetActive(false);

        titleText.text = "Inventory";
    }

}
