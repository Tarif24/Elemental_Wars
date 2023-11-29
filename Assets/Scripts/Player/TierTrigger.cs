using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TierTrigger : MonoBehaviour
{
    public int tier = 0;

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        player.currentTierLocation = tier;
    }
}
