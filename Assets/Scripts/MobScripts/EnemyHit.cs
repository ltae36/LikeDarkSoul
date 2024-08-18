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
            Debug.LogError("Enemy FSM 이 존재하지 않습니다.");

        print(other.tag);
        if (other.CompareTag("Player"))
        {
            playerMove.PlayerHit(damage);   
        }
        else if (other.CompareTag("Shield"))
        {
            fsm.EnemyStateToShield();
        }
    }
}
