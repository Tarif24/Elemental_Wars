using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnitPlayer : MonoBehaviour
{
    public PlayerController player {  get; set; }

    public void SetUp() 
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
}
