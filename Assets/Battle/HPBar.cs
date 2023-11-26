using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar : MonoBehaviour
{
    [SerializeField]
    GameObject health;

    public void SetHP(float hpNormalized) 
    {
        health.transform.localScale = new Vector3(hpNormalized, 1f);
    }

    public IEnumerator SetHPSmooth(float newHp)
    {
        float curHP = health.transform.localScale.x;
        float changeAMT = curHP - newHp;

        while (curHP - newHp > Mathf.Epsilon)
        {
            curHP -= changeAMT * Time.deltaTime;
            health.transform.localScale = new Vector3(curHP, 1f);
            yield return null;
        }

        health.transform.localScale = new Vector3(newHp, 1f);
    }
}
