using System.Collections;
using System.Collections.Generic;
using UnityEditor.Overlays;
using UnityEngine;

public class BossFSM : MonoBehaviour
{ 
    public enum BossState
    {
        Sleep = 1,
        Awake = 2,
        AttackDelay = 4,
        Attack = 8,
        Die = 16
    }

    public enum AttackState
    {
        Horizontal,
        Vertical,
        JumpAttack,
        DashAttack
    }
    [Header("Distance")]
    [SerializeField] float awakeDistance;
    [SerializeField] float attackDistance;

    [Header("Time")]
    [SerializeField] float idleTime;

    [Header("State")]
    [SerializeField]BossState bossState;
    [SerializeField] AttackState attackState;


    float currentTime;

    // Start is called before the first frame update
    void Start()
    {
        bossState = BossState.Sleep;
    }

    // Update is called once per frame
    void Update()
    {
        switch (bossState)
        {
            case BossState.Sleep:
                Sleep();
                break;
            case BossState.Awake:
                WakeUp();
                break;
            case BossState.AttackDelay:
                AttackDelay();
                break;
            case BossState.Attack:
                Attack();
                break;
            case BossState.Die:
                break;
        }
    }

    void Sleep()
    {
        //자고 있는 상태

        //만약 플레이어가 칼을 뽑거나

        //만일 플러이어가 칼을 뽑았는 경우, 일정 거리 내로 들어오면,
        if (IsPlayerWakeBossUp(awakeDistance))
        {
            //상태를 일어난 상태로 바꾼다.
            print("sleep -> awake");
            bossState = BossState.Awake;

            //awake 애니메이션을 실행한다.
            BossAnimationManager.instance.AwakeAnimationStart();
        }
    }

    // 이 함수는 플레이어가 일정 거리안에 들어오면 true를 반환해주는 함수이다.
    bool IsPlayerWakeBossUp(float distance)
    {
        Collider[] colliders =Physics.OverlapSphere(BossLocomotion.instance.myTransform.position,awakeDistance);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                return true;
            }
        }

        return false;
    }


    void WakeUp()
    {
        //일어나는 상태

        //보스 hp바를 활성화시킨다.
        //보스 콜라이더를 활성화시킨다.
        //애니메이션이 끝나면, 공격 딜레이 상태로 전환한다. 
        if (BossAnimationManager.instance.IsAwakeAnimationEnd())
        {
            
            bossState = BossState.AttackDelay;

            //만약 플레이어랑의 거리가 멀다면
            //플레이어한테 다가오는 방향으로 설정한다.
            print("awake -> linear move");
            SelectAttackDelayMovement();
            //만약 플레이어랑 거리가 멀지 않다면,
            //플레이어 좌표를 원의 중심으로 잡고, 방향을 구하면서 움직인다.
        }
    }

    void AttackDelay()
    {
        //공격 대기 시간
        currentTime += Time.deltaTime;
        //이동 목표를 향해서 조금씩 이동한다.
        BossLocomotion.instance.SetMoveDirection(BossLocomotion.MoveType.Linear);
        BossLocomotion.instance.MoveBoss(BossLocomotion.MoveType.Linear);


        //내 몸의 방향을 플레이어를 향하도록 잡는다.
        BossLocomotion.instance.HandleRotation();

        //공격 대기 시간이 지나면, 공격을 한다.
        if (currentTime > idleTime)
        {
            currentTime = 0;
            SelectAttackCombo();
            bossState = BossState.Attack;
            print("Attack delay -> attack");
        }
    }
    
    // 이 함수는 어떤 공격을 할지 정해준다.
    // 정해진 콤보를 상태 함수로 만든다.
    private void SelectAttackCombo()
    {
        BossLocomotion.instance.SetTargetPosition();
        BossLocomotion.instance.SetTargetDirection();
        //만약 거리가 가까우면, horizontal 또는 vertical 공격을 한다.
        print(BossLocomotion.instance.targetDistance);
        if (BossLocomotion.instance.targetDistance < attackDistance)
        {
            int randNum = Random.Range(1, 3);
            if (randNum == 1)
            {
                attackState = AttackState.Horizontal;
                BossAnimationManager.instance.AttackAnimationStart(1, 1);
            }
            else
            {
                attackState = AttackState.Vertical;
                BossAnimationManager.instance.AttackAnimationStart(1, 0);
            }
        }
        //만약 거리가 멀면, jump attack 이나 dash attack을 한다.
        else
        {
            int randNum = Random.Range(1, 3);
            if (randNum == 1)
            {
                attackState = AttackState.JumpAttack;
                BossAnimationManager.instance.AttackAnimationStart(2, 1);
                BossLocomotion.instance.SetMoveDirection(BossLocomotion.MoveType.Jump);
            }
            else
            {
                attackState = AttackState.DashAttack;
                BossAnimationManager.instance.AttackAnimationStart(2, 0);
                BossLocomotion.instance.SetMoveDirection(BossLocomotion.MoveType.Dash);
            }
        }
    }

    void Attack()
    {
        //조건에 따라 공격을 발동한다.
        switch (attackState)
        {
            case AttackState.Horizontal:
                Horizontal();
                break;
            case AttackState.Vertical:
                Vertical();
                break;
            case AttackState.JumpAttack:
                JumpAttack();
                break;
            case AttackState.DashAttack:
                DashAttack();
                break;
        }
        //공격 애니메이션이 끝나면, 공격대기시간으로 전환한다.

    }

    void Horizontal()
    {
        //horizontal 공격을 한다.
        print("Horizontal");
        if(BossAnimationManager.instance.IsAttackAnimationEnd(1,1))
        {
            bossState = BossState.AttackDelay;

            //만약 플레이어랑의 거리가 멀다면
            //플레이어한테 다가오는 방향으로 설정한다.
            print("attack -> linear move");
            SelectAttackDelayMovement();
        }
    }
    void Vertical()
    {
        //Vertical 공격을 한다.
        print("Vertical");
        if (BossAnimationManager.instance.IsAttackAnimationEnd(1, 0))
        {
            bossState = BossState.AttackDelay;

            //만약 플레이어랑의 거리가 멀다면
            //플레이어한테 다가오는 방향으로 설정한다.
            print("attack -> linear move");
            SelectAttackDelayMovement();
        }
    }

    void JumpAttack()
    {
        //jump attack 공격을 한다.
        print("JumpAttack");
        BossLocomotion.instance.MoveBoss(BossLocomotion.MoveType.Jump);
        if(BossLocomotion.instance.IsJumping() == false)
        {
            bossState = BossState.AttackDelay;
            BossAnimationManager.instance.SetTrigger("JumpDown");
            print("attack -> linear move");
            SelectAttackDelayMovement();
        }
    }

    void DashAttack()
    {
        //dash attack 공격을 한다.
        print("DashAttack");
        BossLocomotion.instance.MoveBoss(BossLocomotion.MoveType.Dash);
        if (BossLocomotion.instance.IsDashing() == false)
        {
            bossState = BossState.AttackDelay;
            BossAnimationManager.instance.SetTrigger("RunToWalk");
            print("attack -> linear move");
            SelectAttackDelayMovement();
        }
    }
    void Die()
    {
        //현재 내 hp가 55% 이하라면

        //변신 애니메이션을 실행한다.
        //FSM을 2페이즈 FSM으로 바꾼다.
    }

    //애니메이션이 끝났는지 확인하는 함수
    //나중에 매개변수를 넣을 것이고, 아니면 bossanimationmanager로 차라리 빼버리자
    bool IsAnimationEnd()
    {
        return true;
    }
    void TakeDamage(float damage)
    {

    }
    //거리를 기준으로 앞으로 움직일지 옆으로 움직일 지 선택하는 함수
    void SelectAttackDelayMovement()
    {
        //만약 플레이어랑의 거리가 멀다면
        //플레이어한테 다가오는 방향으로 온다
        //만약 플레이어랑 거리가 멀지 않다면,
        //플레이어 주위로 원을 하나 그린다음, 그 원의 특이한 지점을 잡는다
        BossLocomotion.instance.SetMoveDirection(BossLocomotion.MoveType.Linear);
    }
}
