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
    public GameObject abilitiyChange;

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
        inventory.SetActive(true);
        Time.timeScale = 0;
    }

    public void CloseInventory()
    {
        inventory.SetActive(false);
        Time.timeScale = 1;
    }

    public void HealthPotions()
    {
        if (player.healthPotions > 0 && player.HP < player.MaxHP)
        {
            player.HP = player.MaxHP;
            player.healthPotions--;
        }
    }

    public void AllAbilities()
    {
        inventoryItems.SetActive(false);
        abilitiyChange.SetActive(true);

        titleText.text = "Abilities";
    }

    public void Back()
    {
        inventoryItems.SetActive(true);
        abilitiyChange.SetActive(false);

        titleText.text = "Inventory";
    }

}
