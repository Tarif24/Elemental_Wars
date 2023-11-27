using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Elemental Monster/Create new ability")]
public class AbilitiesBase : ScriptableObject
{
    [SerializeField]
    string name;

    [SerializeField]
    ElementalMonsterType type;

    [SerializeField]
    int damage;

    public string Name
    {
        get { return name; }
    }

    public ElementalMonsterType Type
    {
        get { return type; }
    }

    public int Damage
    {
        get { return damage; }
    }
}
