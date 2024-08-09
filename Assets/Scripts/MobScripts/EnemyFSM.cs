using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM : MonoBehaviour
{
    public enum EnemyState 
    {
        non,
        revival,
        idle,
        chase,
        attack,
        hit,
        death
    }

    public EnemyState undeadState = EnemyState.non;

    void Start()
    {
        
    }

    void Update()
    {
        switch (undeadState) 
        {
            case EnemyState.non:
                break;
            case EnemyState.revival:
                revival();
                break;
            case EnemyState.idle:
                break;
            case EnemyState.chase:
                chase();
                break;
            case EnemyState.attack:
                attack();
                break;
            case EnemyState.hit:
                hitted();
                break;
            case EnemyState.death:
                death();
                break;
        }
    }

    private void death()
    {
        // hp가 0이 되면 사망 애니메이션이 재생되고 래그돌 상태가 된다.
        // 컴포넌트는 비활성화 된다.
    }

    private void hitted()
    {
        // 피격 애니메이션이 재생된다.
        // hp가 감소한다.
    }

    private void chase()
    {
        // 플레이어를 쫓아간다 (NavMeshAgent)
        // 플레이어가 공격 범위 안에 들어오면 attack 상태 전환
        // 플레이어가 시야에서 사라지면 idle 상태 전환
    }

    private void attack()
    {
        // 공격 애니메이션을 재생한다.
        // 플레이어의 거리가 멀어지면 chase 상태 전환
        // 공격을 받으면 hit상태 전환
    }

    private void revival()
    {
        // 플레이어가 일정 범위 안에 들어오면 일어나는 애니메이션이 재생
        // 애니메이션 재생이 끝나면 플레이어를 향해 바라본다
        // attack 상태 전환
    }
}
