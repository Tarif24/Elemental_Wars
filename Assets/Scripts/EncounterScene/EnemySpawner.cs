using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField]
    GameObject[] Enemies;

    [SerializeField]
    Transform EnemyTransform;
     
    void Start()
    {
        if (Enemies != null)
        {
            int RandomNumForEnemySelection = Random.Range(0, Enemies.Length);

            Instantiate(Enemies[RandomNumForEnemySelection], EnemyTransform.position, Quaternion.identity);
        }
        
    }
}
