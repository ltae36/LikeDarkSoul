using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCoroutine : MonoBehaviour
{
    public float hp;

    void Start()
    {
        hp = 200;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H)) 
        {
            StartCoroutine(ConsumptionStat(3));
        }
        
    }

    IEnumerator ConsumptionStat(float sec) 
    {
        print("체력이 감소됨");
        yield return new WaitForSeconds(sec);
        hp -= 10;
        print("체력 : " + hp);

    }
}
