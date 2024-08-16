using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM : FSM
{
    public GameObject awakeTrigger;

    EnemyHealth health;

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
    
    EnemyLocomotion locomotion;
    EnemyAnimationManager animationManager;
    HitCheck check;
    EnemyStatus status;

    float currentTime = 0.0f;
    public float attackDelayTime = 1.0f;

    void Start()
    {
        locomotion = GetComponent<EnemyLocomotion>();
        animationManager = GetComponent<EnemyAnimationManager>();
        check = GetComponent<HitCheck>();
        status = GetComponent<EnemyStatus>();
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

        }
    }

    private void Sleep()
    {
        //�ڰ� �ִ� ����
        //���� �÷��̾ �ڽ� ������ ������ ������...
        Collider[] colliders = Physics.OverlapBox(awakeTrigger.transform.position, awakeTrigger.transform.localScale / 2);

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
        //���¸� awake�� �ٲ۴�.
        //�ִϸ��̼��� �����Ѵ�.
    }

    private void WakeUp()
    {
        //�Ͼ�� ����
        //���� awake �ִϸ��̼��� ������...
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
        //�÷��̾���� �Ÿ��� ����
        //������ ����, �����̸� �Ȱ�, �ָ� ����.
    }
    private void Idle()
    {

        //�ɾ player���� �ٰ����� ����

        //�÷��̾ ���ؼ� �ɾ�´�.
        locomotion.SetMoveDirection(BossLocomotion.MoveType.Linear);
        locomotion.MoveEnemy(BossLocomotion.MoveType.Linear);

        locomotion.HandleRotation();
        //���� �Ÿ��� ���� �Ÿ����� �۾�����

        if(locomotion.targetDistance < attackRange)
        {
            undeadState = EnemyState.AttackDelay;
            animationManager.AttackDelayAnimationStart();
        }
        //�����Ѵ�.
        //���� �Ÿ��� ���� �Ÿ����� �ָ�
        if(locomotion.targetDistance > idleRange)
        {
            undeadState = EnemyState.Run;
            locomotion.SetMoveDirection(BossLocomotion.MoveType.Dash);
            animationManager.RunAnimationStart();
        }
    }

    //�޸���.
    private void Chase()
    {
        // �ھ player���� �ٰ����� ����

        locomotion.SetMoveDirection(BossLocomotion.MoveType.Dash);
        //�÷��̾ ���ؼ� �پ�´�.
        locomotion.MoveEnemy(BossLocomotion.MoveType.Dash);

        locomotion.HandleRotation();
        //���� �Ÿ��� ���� �Ÿ����� �۾�����

        if(locomotion.targetDistance < attackRange)
        {
            undeadState = EnemyState.Attack;
            animationManager.AttackAnimationStart();
        }
        //�����Ѵ�.
    }
    private void Attack()
    {
        // �����ϴ� ����
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
        //���ݾִϸ��̼��� ������, ���� ��� ���·� �Ѿ��.
    }

    private void AttackDelay()
    {
        locomotion.HandleRotation();
        currentTime += Time.deltaTime;

        locomotion.SetTargetPosition();
        locomotion.SetTargetDirection();

        if (currentTime>attackDelayTime)
        {
            undeadState = EnemyState.Attack;
            animationManager.AttackAnimationStart();
            currentTime = 0;
        }       
    }
    private void Hitted()
    {
        // �ǰ� �ִϸ��̼��� ����ȴ�.
        animationManager.HitAnimationStart();

    }

    public void Death()
    {
        // hp�� 0�� �Ǹ� ��� �ִϸ��̼��� ����ǰ� ���׵� ���°� �ȴ�.
        // ������Ʈ�� ��Ȱ��ȭ �ȴ�.
    }

    public void TakeDamage(int damage)
    {

        //���� ���� ���� ���� ��Ȳ�� �ƴ϶��...
        if(undeadState != EnemyState.Hit && undeadState != EnemyState.Die)
        {
            status.health -= damage;

            undeadState = EnemyState.Hit;

            if(status.health <= 0)
            {
                undeadState = EnemyState.Die;
            }
        }

    }
}
