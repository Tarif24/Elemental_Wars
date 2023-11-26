using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomEncounter : MonoBehaviour
{
    [SerializeField]
    string targetScene;

    PlayerController player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int RandomNumForEncounter = Random.Range(0, 101);

        player = FindObjectOfType<PlayerController>();

        if (collision.tag == "Player" && RandomNumForEncounter <= 5 && !player.isSafe && !player.isInEncounter) 
        {
            collision.gameObject.GetComponent<PlayerController>().isInEncounter = true;
            collision.gameObject.GetComponent<PlayerController>().isSafe = true;

            SceneManager.LoadScene(2, LoadSceneMode.Additive);
        }
    }
}
