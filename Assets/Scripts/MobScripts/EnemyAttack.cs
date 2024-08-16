using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyFSM;

public class EnemyAttack : DamageCount
{
    public float damage;

    public FSM fsm;
    EnemyFSM enemyFSM;
    BossFSM bossFSM;

    private void Start()
    {
        if (fsm == null)
            Debug.LogError("Fsm �� �����ϴ�!");
        else
        {
            if(fsm is EnemyFSM)
            {
                enemyFSM = (EnemyFSM)fsm;
            }
            if(fsm is BossFSM)
            {
                bossFSM = (BossFSM)fsm;
            }
        }
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
            if (other.gameObject.GetComponentInChildren<StatManager>() != null )
            {
                if (fsm is EnemyFSM && enemyFSM.undeadState == EnemyState.Attack )
                {
                    other.gameObject.GetComponentInChildren<StatManager>().HP -= damage;
                }
                else if(fsm is BossFSM && bossFSM.bossState == BossFSM.BossState.Attack)
                {
                    other.gameObject.GetComponentInChildren<StatManager>().HP -= damage;
                }
            }
        }
    }
}
