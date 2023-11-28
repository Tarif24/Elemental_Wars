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

    public bool isBoss = false;

    public int[] bossLevels;
    public ElementalMonsterBase[] bossBases;

    public void SetUp()
    {
        if (isBoss)
        {
            BossSetUp();
        }
        else
        {
            MonsterSetUp();
        }
    }

    void BossSetUp()
    {
        PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        main = bossBases[player.bossTier];
        monster = new ElementalMonster(main, bossLevels[player.bossTier]);

        GetComponent<Image>().sprite = monster.main.MonsterSprite;

    }

    void MonsterSetUp()
    {
        monster = new ElementalMonster(main, level);

        GetComponent<Image>().sprite = monster.main.MonsterSprite;
    }
}
