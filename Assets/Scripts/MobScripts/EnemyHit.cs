using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    public EnemyFSM fsm;
    public PlayerMove playerMove;

    public float damage = 30.0f;

    private void OnTriggerEnter(Collider other)
    {
        if (fsm == null)
            Debug.LogError("Enemy FSM �� �������� �ʽ��ϴ�.");

        print(other.tag);
        if (other.CompareTag("Shield"))
        {
            fsm.EnemyStateToShield();
        }
        else if (other.CompareTag("Player"))
        {
            playerMove.PlayerHit(damage);   
        }
    }
}
