using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyFSM : FSM
{
    public GameObject awakeTrigger;
    public GameObject healthBar;

    public GameObject enemyRagdoll;
    public GameObject enemyModeling;
    public BoxCollider swordCollider;

    public enum EnemyState 
    {
        Sleep,
        Awake,
        Idle,
        Run,
        Attack,
        AttackDelay,
        Hit,
        Die,
        Shield
    }

    public EnemyState undeadState;
    public float attackRange;
    public float idleRange;
    
    EnemyLocomotion locomotion;
    EnemyAnimationManager animationManager;
    HitCheck check;
    EnemyStatus status;
    CharacterController cc;

    float currentTime = 0.0f;
    public float attackDelayTime = 1.0f;

    void Start()
    {
        healthBar.SetActive(false);
        locomotion = GetComponent<EnemyLocomotion>();
        animationManager = GetComponent<EnemyAnimationManager>();
        check = GetComponent<HitCheck>();
        status = GetComponent<EnemyStatus>();
        cc= GetComponent<CharacterController>();
        enemyType = EnemyType.Enemy;
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
                Hitted();
                break;
            case EnemyState.Die:
                Death();
                break;
            case EnemyState.Shield:
                Shield();
                break;

        }
    }

    public void EnemyStateToShield()
    {
        undeadState = EnemyState.Shield;
        animationManager.ShieldAnimationStart();
    }
    private void Shield()
    {
        //shield animation을 재생한다.

        //만약 shield animation이 끝나면
        if (animationManager.IsShieldAnimationEnd())
        {
            //어택 딜레이로 넘어간다.
            undeadState = EnemyState.AttackDelay;
            animationManager.AttackDelayAnimationStart();
        }
    }

    private void Sleep()
    {
        //자고 있는 상태
        //만약 플레이어가 박스 오버랩 안으로 들어오면...
        Collider[] colliders = Physics.OverlapBox(awakeTrigger.transform.position, awakeTrigger.transform.localScale / 2);

        foreach (Collider collider in colliders)
        {
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

            healthBar.SetActive(true);

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
        locomotion.MoveEnemy(BossLocomotion.MoveType.Linear);

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
    }

    //달린다.
    private void Chase()
    {
        // 뒤어서 player에게 다가오는 상태

        locomotion.SetMoveDirection(BossLocomotion.MoveType.Dash);
        //플레이어를 향해서 뛰어온다.
        locomotion.MoveEnemy(BossLocomotion.MoveType.Dash);

        locomotion.HandleRotation();
        //만약 거리가 일정 거리보다 작아지면

        if(locomotion.targetDistance < attackRange)
        {
            swordCollider.enabled = true;
            undeadState = EnemyState.Attack;
            animationManager.AttackAnimationStart();
        }
        //공격한다.
    }
    private void Attack()
    {
        // 공격하는 상태
        if (animationManager.IsAttackAnimationEnd())
        {
            if (locomotion.targetDistance > idleRange)
            {
                undeadState = EnemyState.Run;
                locomotion.SetMoveDirection(BossLocomotion.MoveType.Dash);
                animationManager.RunAnimationStart();
            }
            else if (locomotion.targetDistance > attackRange)
            {
                undeadState = EnemyState.Idle;
                locomotion.SetMoveDirection(BossLocomotion.MoveType.Linear);
                animationManager.IdleAnimationStart();
            }
            else
            {
                undeadState = EnemyState.AttackDelay;
                animationManager.AttackDelayAnimationStart();
            }
        }
        //공격애니메이션이 끝나면, 공격 대기 상태로 넘어간다.
    }

    private void AttackDelay()
    {
        locomotion.HandleRotation();
        currentTime += Time.deltaTime;

        locomotion.SetTargetPosition();
        locomotion.SetTargetDirection();

        if (currentTime>attackDelayTime)
        {
            swordCollider.enabled=true;
            undeadState = EnemyState.Attack;
            animationManager.AttackAnimationStart();
            currentTime = 0;
        }       
    }
    private void Hitted()
    {
        if (animationManager.IsHitAnimationEnd())
        {
            undeadState = EnemyState.AttackDelay;

        }
    }

    public void Death()
    {
        if (animationManager.IsHitAnimationFullyEnd())
        {
            // hp가 0이 되면 사망 애니메이션이 재생되고 래그돌 상태가 된다.
            // 컴포넌트는 비활성화 된다.
            CopyAnimCharacterTransformToRagdoll(enemyModeling.transform, enemyRagdoll.transform);
            enemyModeling.SetActive(false);
            GetComponent<CharacterController>().enabled = false;
            enemyRagdoll.SetActive(true);

            this.enabled = false;
        }
    }

    public void TakeDamage(int damage)
    {

        //만약 지금 공격 받은 상황이 아니라면...
        if(undeadState != EnemyState.Hit && undeadState != EnemyState.Die)
        {
            status.health -= damage;

            undeadState = EnemyState.Hit;

            // 피격 애니메이션이 재생된다.
            animationManager.HitAnimationStart();

            if (status.health <= 0)
            {
                animationManager.DeadAnimationStart();
                //레그돌 상태가 된다.
                undeadState = EnemyState.Die;
            }
        }

    }
    void CopyAnimCharacterTransformToRagdoll(Transform origin, Transform rag) 
    {
        rag.transform.SetLocalPositionAndRotation(origin.transform.localPosition, origin.transform.localRotation);

        //print("origin vs rag" + origin.transform.childCount+" " + rag.transform.childCount+" "+(origin.transform.childCount == rag.transform.childCount));
        for (int i = 0; i < origin.transform.childCount; i++) 
        {
            CopyAnimCharacterTransformToRagdoll(origin.transform.GetChild(i), rag.transform.GetChild(i)); 
        } 
    }
}
