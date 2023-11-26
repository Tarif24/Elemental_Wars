using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUnitMonster : MonoBehaviour
{
    [SerializeField]
    ElementalMonsterBase main;

    [SerializeField]
    int level;

    public ElementalMonster monster {  get; set; }

    public void SetUp()
    {
        monster = new ElementalMonster(main, level);

        GetComponent<Image>().sprite = monster.main.MonsterSprite;
    }
}
