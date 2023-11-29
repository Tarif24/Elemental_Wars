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
    
    ElementalMonsterType strength;
   
    ElementalMonsterType weakness;

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

    public ElementalMonsterType Strength
    {
        get 
        {
            switch (type)
            {
                case ElementalMonsterType.Fire:
                    strength = ElementalMonsterType.Air;
                    break;
                case ElementalMonsterType.Air:
                    strength = ElementalMonsterType.Earth;
                    break;
                case ElementalMonsterType.Water:
                    strength = ElementalMonsterType.Fire;
                    break;
                case ElementalMonsterType.Earth:
                    strength = ElementalMonsterType.Electric;
                    break;
                case ElementalMonsterType.Electric:
                    strength = ElementalMonsterType.Water;
                    break;
                default:
                    strength = ElementalMonsterType.None;
                    break;

            }

            return strength;
        }
    }

    public ElementalMonsterType Weakness
    {
        get
        {
            switch (type)
            {
                case ElementalMonsterType.Fire:
                    weakness = ElementalMonsterType.Water;
                    break;
                case ElementalMonsterType.Air:
                    weakness = ElementalMonsterType.Fire;
                    break;
                case ElementalMonsterType.Water:
                    weakness = ElementalMonsterType.Electric;
                    break;
                case ElementalMonsterType.Earth:
                    weakness = ElementalMonsterType.Air;
                    break;
                case ElementalMonsterType.Electric:
                    weakness = ElementalMonsterType.Earth;
                    break;
                default:
                    weakness = ElementalMonsterType.None;
                    break;

            }
            return weakness;    
        }
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
