using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleHudPlayer : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI nameText;
    [SerializeField]
    TextMeshProUGUI levelText;
    [SerializeField]
    TextMeshProUGUI typeText;
    [SerializeField]
    HPBar hpBar;

    PlayerController Player;

    public void SetData(PlayerController player) 
    {
        Player = player;

        nameText.text = player.Name;
        levelText.text = "Level " + player.Level;
        typeText.text = "Type " + player.type.ToString();
        hpBar.SetHP((float) player.HP / player.MaxHP);
    }

    public IEnumerator SetHP()
    {
        yield return hpBar.SetHPSmooth((float)Player.HP / Player.MaxHP);
    }
}
