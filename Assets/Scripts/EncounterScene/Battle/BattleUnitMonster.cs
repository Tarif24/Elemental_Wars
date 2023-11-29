using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUnitMonster : MonoBehaviour
{
    [SerializeField]
    ElementalMonsterBase main;

    int level;

    PlayerController player;

    public ElementalMonster monster {  get; set; }

    public bool isBoss = false;

    public int[] bossLevels;
    public ElementalMonsterBase[] bossBases;

    public void SetUp()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

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
        main = bossBases[player.currentTierLocation];
        monster = new ElementalMonster(main, bossLevels[player.currentTierLocation]);

        GetComponent<Image>().sprite = monster.main.MonsterSprite;

    }

    void MonsterSetUp()
    {
        switch (player.currentTierLocation)
        {
            case 0:
                level = Random.Range(1, 10);
                break;
            case 1:
                level = Random.Range(10, 20);
                break;
            case 2:
                level = Random.Range(20, 30);
                break;
            case 3:
                level = Random.Range(30, 40);
                break;
            case 4:
                level = Random.Range(40, 50);
                break;
            default:
                break;
        }

        monster = new ElementalMonster(main, level);

        GetComponent<Image>().sprite = monster.main.MonsterSprite;
    }
}
