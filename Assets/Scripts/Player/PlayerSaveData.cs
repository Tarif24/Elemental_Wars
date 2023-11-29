using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SO_PlayerSaveData : ScriptableObject
{
    [SerializeField]
    public bool isSafe;
    [SerializeField]
    public bool isSaveGame;
}
