using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SO_PlayerSaveData : ScriptableObject
{
    [SerializeField]
    public bool isSafe;
    [SerializeField]
    public Vector3 saveLocation;
    [SerializeField]
    public bool isSaveGame;
    [SerializeField]
    public string name;
    [SerializeField]
    public List<AbilitiesBase> abilities;
    [SerializeField]
    public int level;
    [SerializeField]
    public int elementalCoins;
    [SerializeField]
    public int bossTier;
    [SerializeField]
    public int hp;
    [SerializeField]
    public int currentTierLocation;
}
