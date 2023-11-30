using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossShrine : MonoBehaviour
{
    public int bossTier = 0;

    PlayerController player;

    [SerializeField]
    bool isActive = false;
    [SerializeField]
    bool inRange = false;

    public GameObject inactive;
    public GameObject active;
    public GameObject defeated;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.F) && isActive)
        {
            player.GetComponent<PlayerController>().isInEncounter = true;

            SceneManager.LoadScene(3, LoadSceneMode.Additive);
        }
    }

    private void FixedUpdate()
    {
        if (player.bossTier + 1 >= bossTier)
        {
            isActive = true;
        }

        if (player.bossTier + 1 == bossTier)
        {
            inactive.SetActive(false);
            active.SetActive(true);
            defeated.SetActive(false);
        }
        else if (player.bossTier >= bossTier)
        {
            inactive.SetActive(false);
            active.SetActive(false);
            defeated.SetActive(true);
        }
        else
        {
            inactive.SetActive(true);
            active.SetActive(false);
            defeated.SetActive(false);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = false;
        }
    }
}
