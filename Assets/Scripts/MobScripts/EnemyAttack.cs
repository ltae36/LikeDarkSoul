using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyFSM;

public class EnemyAttack : DamageCount
{
    public float damage;

    public EnemyFSM fsm;
    EnemyState state;

    private void Start()
    {
        fsm = GetComponentInParent<EnemyFSM>();
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
            if (other.gameObject.GetComponentInChildren<StatManager>() != null && fsm.undeadState == EnemyState.Attack)
            {
                other.gameObject.GetComponentInChildren<StatManager>().HP -= damage;
            }
        }
    }
}
