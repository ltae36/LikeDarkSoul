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
            // 방패에 맞으면 공격 실패 애니메이션이 재생된다.
            
        }
        else if (other.gameObject.tag == "Player") 
        {
            // 플레이어에게 맞으면 플레이어에게 대미지를 준다.
            if (other.gameObject.GetComponentInChildren<StatManager>() != null && fsm.undeadState == EnemyState.Attack)
            {
                other.gameObject.GetComponentInChildren<StatManager>().HP -= damage;
            }
        }
    }
}
