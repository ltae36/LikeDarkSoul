using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : DamageCount
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Shield") 
        {
            // ���п� ������ ���� ���� �ִϸ��̼��� ����ȴ�.
            
        }
        else if (other.gameObject.tag == "Player") 
        {
            // �÷��̾�� ������ �÷��̾�� ������� �ش�.
        }
    }
}
