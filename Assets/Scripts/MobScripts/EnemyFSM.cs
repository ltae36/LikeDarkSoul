using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM : MonoBehaviour
{

    public GameObject awakeTrigger;

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
    public float attackRange;
    public float idleRange;
    BossLocomotion locomotion;
    EnemyAnimationManager animationManager;
    float currentTime = 0.0f;
    public float attackDelayTime = 1.0f;

    void Start()
    {
        locomotion = GetComponent<BossLocomotion>();
        animationManager = GetComponent<EnemyAnimationManager>();
    }

    void Update()
    {
        switch (undeadState) 
        {
            case EnemyState.Sleep:
                Sleep();
                break;
            case EnemyState.Awake:
                WakeUp();
                break;
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Run:
                Chase();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.AttackDelay:
                AttackDelay();
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
        //만약 플레이어가 박스 오버랩 안으로 들어오면...
        Collider[] colliders = Physics.OverlapBox(awakeTrigger.transform.position,awakeTrigger.transform.localScale/2) ;

        foreach (Collider collider in colliders)
        {
            print(collider.gameObject.name);
            if (collider.gameObject.CompareTag("Player"))
            {
                undeadState = EnemyState.Awake;
                print("sleep -> awake");
                animationManager.AwakeAnimationStart();
            }
        }
        //상태를 awake로 바꾼다.
        //애니메이션을 실행한다.
    }

    private void WakeUp()
    {
        //일어나는 상태
        //만약 awake 애니메이션이 끝나면...
        if (animationManager.IsAwakeAnimationEnd())
        {
            locomotion.SetTargetPosition();
            locomotion.SetTargetDirection();

            if(locomotion.targetDistance < attackRange)
            {
                undeadState = EnemyState.AttackDelay;
                animationManager.AttackDelayAnimationStart();

            }
            else if (locomotion.targetDistance < idleRange)
            {
                undeadState = EnemyState.Idle;
                locomotion.SetMoveDirection(BossLocomotion.MoveType.Linear);
                animationManager.IdleAnimationStart();
            }
            else
            {
                undeadState = EnemyState.Run;
                locomotion.SetMoveDirection(BossLocomotion.MoveType.Dash);
                animationManager.RunAnimationStart();
            }
        }
        //플레이어와의 거리에 따라
        //가까우면 공격, 보통이면 걷고, 멀면 뛰자.
    }
    private void Idle()
    {
        //걸어서 player에게 다가오는 상태

        //플레이어를 향해서 걸어온다.
        locomotion.SetMoveDirection(BossLocomotion.MoveType.Linear);
        locomotion.MoveBoss(BossLocomotion.MoveType.Linear);

        locomotion.HandleRotation();
        //만약 거리가 일정 거리보다 작아지면

        if(locomotion.targetDistance < attackRange)
        {
            undeadState = EnemyState.AttackDelay;
            animationManager.AttackDelayAnimationStart();
        }
        //공격한다.
        //만약 거리가 일정 거리보다 멀면
        if(locomotion.targetDistance > idleRange)
        {
            undeadState = EnemyState.Run;
            locomotion.SetMoveDirection(BossLocomotion.MoveType.Dash);
            animationManager.RunAnimationStart();
        }
        //달린다.
    }
    private void Chase()
    {
        // 뒤어서 player에게 다가오는 상태

        //플레이어를 향해서 뛰어온다.
        locomotion.MoveBoss(BossLocomotion.MoveType.Dash);

        locomotion.HandleRotation();
        //만약 거리가 일정 거리보다 작아지면

        if(locomotion.targetDistance < attackRange)
        {
            undeadState = EnemyState.AttackDelay;
            animationManager.AttackDelayAnimationStart();
        }
        //공격한다.
    }
    private void Attack()
    {
        // 공격하는 상태
        if (animationManager.IsAttackAnimationEnd())
        {
            undeadState = EnemyState.AttackDelay;
        }
        //공격애니메이션이 끝나면, 공격 대기 상태로 넘어간다.
    }

    private void AttackDelay()
    {
        currentTime += Time.deltaTime;

        locomotion.SetTargetPosition();
        locomotion.SetTargetDirection();
        if(locomotion.targetDistance > attackRange)
        {
            undeadState = EnemyState.Idle;
            animationManager.IdleAnimationStart();
            currentTime = 0;
        }
        else if (currentTime>attackDelayTime)
        {
            undeadState = EnemyState.Attack;
            animationManager.AttackAnimationStart();
            currentTime = 0;
        }
        
        
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
