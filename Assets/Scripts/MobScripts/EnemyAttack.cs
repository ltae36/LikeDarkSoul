using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : DamageCount
{
    public float damage;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Shield") 
        {
            // ���п� ������ ���� ���� �ִϸ��̼��� ����ȴ�.
            
        }
        else if (other.gameObject.tag == "Player") 
        {
            // �÷��̾�� ������ �÷��̾�� ������� �ش�.
            if (other.gameObject.GetComponentInChildren<StatManager>() != null)
            {
                other.gameObject.GetComponentInChildren<StatManager>().HP -= damage;
            }
        }
    }
}
