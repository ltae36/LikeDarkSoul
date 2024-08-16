using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    EnemyFSM fsm;

    private void Start()
    {
        fsm = GetComponentInParent<EnemyFSM>();
    }
    public float damage = 0.0f;
    private void OnTriggerEnter(Collider other)
    {
        if (fsm == null)
            Debug.LogError("Enemy FSM �� �������� �ʽ��ϴ�.");

        if (other.gameObject.tag.Contains("Player") && fsm?.undeadState == EnemyFSM.EnemyState.Attack)
        {
            other.gameObject.GetComponentInChildren<StatManager>().HP -= damage;   
        }
    }
}
