using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM : MonoBehaviour
{
    public enum EnemyState 
    {
        Sleep,
        Awake,
        Idle,
        Run,
        Attack,
        AttackDelay,
        Hit,
        Die
    }

    public EnemyState undeadState;

    void Start()
    {
        
    }

    void Update()
    {
        switch (undeadState) 
        {
            case EnemyState.Sleep:
                break;
            case EnemyState.Awake:
                break;
            case EnemyState.Idle:
                break;
            case EnemyState.Run:
                break;
            case EnemyState.Attack:        
                break;
            case EnemyState.AttackDelay:
                break;
            case EnemyState.Hit:
                break;
            case EnemyState.Die:
                break;

        }
    }

    private void Sleep()
    {
        //자고 있는 상태
        //만약 플레이어가 일정 거리 내로 들어오면...
        //상태를 awake로 바꾼다.
        //애니메이션을 실행한다.
    }

    private void Awake()
    {
        //일어나는 상태
        //만약 awake 애니메이션이 끝나면...
        //플레이어와의 거리에 따라
        //가까우면 공격, 보통이면 걷고, 멀면 뛰자.
    }
    private void Idle()
    {
        //걸어서 player에게 다가오는 상태

        //플레이어를 향해서 걸어온다.
        //만약 거리가 일정 거리보다 작아지면
        //공격한다.
        //만약 거리가 일정 거리보다 멀면
        //달린다.
    }
    private void Chase()
    {
        // 뒤어서 player에게 다가오는 상태

        //플레이어를 향해서 뛰어온다.
        //만약 거리가 일정 거리보다 작아지면
        //공격한다.
    }
    private void Attack()
    {
        // 공격하는 상태

        //공격애니메이션이 끝나면, 공격 대기 상태로 넘어간다.
    }

    private void AttackDelay()
    {

    }
    private void Hitted()
    {
        // 피격 애니메이션이 재생된다.
        // hp가 감소한다.
    }

    private void Death()
    {
        // hp가 0이 되면 사망 애니메이션이 재생되고 래그돌 상태가 된다.
        // 컴포넌트는 비활성화 된다.
    }
}
