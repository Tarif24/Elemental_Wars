using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class BattleHudMonster : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI nameText;
    [SerializeField]
    TextMeshProUGUI levelText;
    [SerializeField]
    TextMeshProUGUI typeText;
    [SerializeField]
    HPBar hpBar;

    ElementalMonster Monster;

    public void SetData(ElementalMonster monster)
    {
        Monster = monster;

        nameText.text = monster.main.Name;
        levelText.text = "Level " + monster.level;
        typeText.text = "Type " + monster.main.Type.ToString();
        hpBar.SetHP((float) monster.HP / monster.MaxHP);
    }

    public IEnumerator SetHP()
    {
        yield return hpBar.SetHPSmooth((float)Monster.HP / Monster.MaxHP);
    }
}
