using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    public GameObject shop;
    public GameObject shopItems;
    public GameObject elementChange;

    PlayerController player;

    public AudioSource buttonSound;

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
        buttonSound.Play();
        shop.SetActive(true);
        player.isBusy = true;
        Time.timeScale = 0;
    }

    public void CloseShop()
    {
        buttonSound.Play();
        shop.SetActive(false);
        player.isBusy = false;
        Time.timeScale = 1;
    }

    public void HealthPotion()
    {
        buttonSound.Play();
        if (player.elementalCoins - 75 >= 0)
        {
            player.elementalCoins -= 75;
            player.healthPotions++;
        }
    }

    public void XP()
    {
        buttonSound.Play();
        if (player.elementalCoins - 150 >= 0)
        {
            player.elementalCoins -= 150;
            player.XP += 100;
        }
    }

    public void ElementChange()
    {
        buttonSound.Play();
        if (player.elementalCoins - 250 >= 0)
        {
            player.elementalCoins -= 250;

            shopItems.SetActive(false);
            elementChange.SetActive(true);
        }
    }

    public void Fire()
    {
        buttonSound.Play();
        player.ChangeType(ElementalMonsterType.Fire);
        shopItems.SetActive(true);
        elementChange.SetActive(false);
    }

    public void Air()
    {
        buttonSound.Play();
        player.ChangeType(ElementalMonsterType.Air);
        shopItems.SetActive(true);
        elementChange.SetActive(false);
    }

    public void Water()
    {
        buttonSound.Play();
        player.ChangeType(ElementalMonsterType.Water);
        shopItems.SetActive(true);
        elementChange.SetActive(false);
    }

    public void Earth()
    {
        buttonSound.Play();
        player.ChangeType(ElementalMonsterType.Earth);
        shopItems.SetActive(true);
        elementChange.SetActive(false);
    }

    public void Electric()
    {
        buttonSound.Play();
        player.ChangeType(ElementalMonsterType.Electric);
        shopItems.SetActive(true);
        elementChange.SetActive(false);
    }
}
