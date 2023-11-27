using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SO_PlayerSaveData : ScriptableObject
{
    public bool isSafe = false;
    public Vector3 saveLocation;
    public bool isSaveGame = false;
}
