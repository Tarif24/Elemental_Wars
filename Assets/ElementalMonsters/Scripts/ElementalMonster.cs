using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalMonster
{
    public ElementalMonsterBase main { get; set; }
    public int level { get; set; }

    public int HP { get; set; }

    public List<AbilitiesBase> Abilities;

    public ElementalMonster (ElementalMonsterBase mMain, int mLevel)
    {
        main = mMain;
        level = mLevel;
        HP = MaxHP;

        Abilities = main.Abilities;
    }

    public int Attack
    {
        get { return main.Attack; }
    }

    public int Defense
    {
        get { return main.Defense; }
    }

    public int MaxHP
    {
        get { return main.MaxHP; }
    }

    public bool TakeDamage(AbilitiesBase ability, PlayerController player)
    {
        HP -= ability.Damage;

        if (HP <= 0)
        {
            HP = 0;

            return true;
        }
        else
        {
            return false;
        }
    }

    public AbilitiesBase GetComputerMove()
    {
        AbilitiesBase finalMove = Abilities[0];
        AbilitiesBase typeMove = null;
        int rand;

        for (int i = 0; i < Abilities.Count; i++)
        {
            if (main.Type == Abilities[i].Type)
            {
                if (typeMove != null)
                {
                    if (typeMove.Damage < Abilities[i].Damage)
                    {
                        typeMove = Abilities[i];
                    }
                }
                else
                {
                    typeMove = Abilities[i];
                }
            }
        }

        if (typeMove != null)
        {
            rand = Random.Range(0, 101);
        }
        else
        {
            rand = Random.Range(0, 50);
        }

        if (rand >= 50)
        {
            finalMove = typeMove;
        }
        else if (rand >= 20)
        {
            for (int i = 0; i < Abilities.Count; i++)
            {
                if (finalMove.Damage < Abilities[i].Damage)
                {
                    finalMove = Abilities[i];
                }
            }
        }
        else
        {
            int r = Random.Range(0, Abilities.Count);
            finalMove = Abilities[r];
        }

        return finalMove;
    }
}
