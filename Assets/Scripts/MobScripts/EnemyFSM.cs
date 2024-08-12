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
    HitCheck check;

    float currentTime = 0.0f;
    public float attackDelayTime = 1.0f;

    void Start()
    {
        locomotion = GetComponent<BossLocomotion>();
        animationManager = GetComponent<EnemyAnimationManager>();
        check = GetComponent<HitCheck>();
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
        //�ڰ� �ִ� ����
        //���� �÷��̾ �ڽ� ������ ������ ������...
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
        if (check.enemyDamaged) 
        {
            undeadState = EnemyState.Hit;
        }

        //�ɾ player���� �ٰ����� ����

        //�÷��̾ ���ؼ� �ɾ�´�.
        locomotion.SetMoveDirection(BossLocomotion.MoveType.Linear);
        locomotion.MoveBoss(BossLocomotion.MoveType.Linear);

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
        //�޸���.
    }
    private void Chase()
    {
        // �ھ player���� �ٰ����� ����

        //�÷��̾ ���ؼ� �پ�´�.
        locomotion.MoveBoss(BossLocomotion.MoveType.Dash);

        locomotion.HandleRotation();
        //���� �Ÿ��� ���� �Ÿ����� �۾�����

        if(locomotion.targetDistance < attackRange)
        {
            undeadState = EnemyState.AttackDelay;
            animationManager.AttackDelayAnimationStart();
        }
        //�����Ѵ�.
    }
    private void Attack()
    {
        // �����ϴ� ����
        if (animationManager.IsAttackAnimationEnd())
        {
            undeadState = EnemyState.AttackDelay;
        }
        //���ݾִϸ��̼��� ������, ���� ��� ���·� �Ѿ��.
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

        if (check.enemyDamaged) 
        {
            undeadState = EnemyState.Hit;
        }
        
        
    }
    private void Hitted()
    {
        // �ǰ� �ִϸ��̼��� ����ȴ�.
        animationManager.HitAnimationStart();
        // hp�� �����Ѵ�.

        if (!check.enemyDamaged) 
        {
            undeadState = EnemyState.AttackDelay;
        }
    }

    private void Death()
    {
        // hp�� 0�� �Ǹ� ��� �ִϸ��̼��� ����ǰ� ���׵� ���°� �ȴ�.
        // ������Ʈ�� ��Ȱ��ȭ �ȴ�.
    }
}
