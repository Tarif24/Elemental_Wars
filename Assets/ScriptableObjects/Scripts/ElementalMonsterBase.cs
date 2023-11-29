using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Elemental Monster", menuName = "Elemental Monster/Create New Monster")]
public class ElementalMonsterBase : ScriptableObject
{
    [SerializeField]
    string name;

    [SerializeField]
    Sprite monsterSprite;

    [SerializeField]
    ElementalMonsterType type;

    [SerializeField]
    int maxHP;
    [SerializeField]
    int attack;
    [SerializeField]
    int defense;

    [SerializeField]
    List<AbilitiesBase> abilities;

    public List<AbilitiesBase> Abilities 
    { 
        get { return abilities; } 
    }

    public string Name
    {
        get { return name; }
    }

    public Sprite MonsterSprite
    {
        get { return monsterSprite; }
    }

    public ElementalMonsterType Type
    {
        get { return type; }
    }

    public int MaxHP
    {
        get { return maxHP; }
    }

    public int Attack
    {
        get { return attack; }
    }

    public int Defense
    {
        get { return defense; }
    }
}

public enum ElementalMonsterType 
{
    None,
    Fire,
    Water,
    Earth,
    Air,
    Electric

}
