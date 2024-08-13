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
        print("ü���� ���ҵ�");
        yield return new WaitForSeconds(sec);
        hp -= 10;
        print("ü�� : " + hp);

    }
}
