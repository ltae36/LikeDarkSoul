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
            Debug.LogError("Fsm 이 없습니다!");
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
        print(other.gameObject.name);
        if(other.gameObject.tag == "Shield") 
        {
            // 방패에 맞으면 공격 실패 애니메이션이 재생된다.
            
        }
        else if (other.gameObject.tag == "Player") 
        {
            // 플레이어에게 맞으면 플레이어에게 대미지를 준다.

            PlayerMove player = other.gameObject.GetComponent<PlayerMove>();

            if (player != null )
            {
                if (fsm is EnemyFSM && enemyFSM.undeadState == EnemyState.Attack )
                {
                    player.PlayerHit(damage);
                }
                else if(fsm is BossFSM && bossFSM.bossState == BossFSM.BossState.Attack)
                {
                    player.PlayerHit(damage);
                }
            }

            else
            {
                Debug.LogError("player 의 PlayerMove를 찾을 수 없습니다");
            }
        }
    }
}
