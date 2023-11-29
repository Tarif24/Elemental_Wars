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
        
    }

    public void OpenShop()
    {
        shop.SetActive(true);
        Time.timeScale = 0;
    }

    public void CloseShop()
    {
        shop.SetActive(true);
        Time.timeScale = 1;
    }

    public void HealthPotion()
    {
        
    }

    public void XP()
    {

    }

    public void ElementChange()
    {

    }
}
