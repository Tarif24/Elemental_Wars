using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    public GameObject shop;
    public GameObject shopItems;
    public GameObject elementChange;

    PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && player.inShopRange)
        {
            OpenShop();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseShop();
        }
    }

    public void OpenShop()
    {
        shop.SetActive(true);
        Time.timeScale = 0;
    }

    public void CloseShop()
    {
        shop.SetActive(false);
        Time.timeScale = 1;
    }

    public void HealthPotion()
    {
        if (player.elementalCoins - 75 >= 0)
        {
            player.elementalCoins -= 75;
            player.healthPotions++;
        }
    }

    public void XP()
    {
        if (player.elementalCoins - 150 >= 0)
        {
            player.elementalCoins -= 150;
            player.XP += 100;
        }
    }

    public void ElementChange()
    {
        if (player.elementalCoins - 250 >= 0)
        {
            player.elementalCoins -= 250;

            shopItems.SetActive(false);
            elementChange.SetActive(true);
        }
    }

    public void Fire()
    {
        player.type = ElementalMonsterType.Fire;
        shopItems.SetActive(true);
        elementChange.SetActive(false);
    }

    public void Air()
    {
        player.type = ElementalMonsterType.Air;
        shopItems.SetActive(true);
        elementChange.SetActive(false);
    }

    public void Water()
    {
        player.type = ElementalMonsterType.Water;
        shopItems.SetActive(true);
        elementChange.SetActive(false);
    }

    public void Earth()
    {
        player.type = ElementalMonsterType.Earth;
        shopItems.SetActive(true);
        elementChange.SetActive(false);
    }

    public void Electric()
    {
        player.type = ElementalMonsterType.Electric;
        shopItems.SetActive(true);
        elementChange.SetActive(false);
    }
}
