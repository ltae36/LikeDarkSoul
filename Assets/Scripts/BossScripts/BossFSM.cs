using System.Collections;
using System.Collections.Generic;
using UnityEditor.TerrainTools;
using UnityEngine;

struct AttackUnit
{
    public BossFSM.AttackState attack;

    public AttackUnit( BossFSM.AttackState attack)
    {
        this.attack = attack;
    }
}


public class BossFSM : FSM
{ 
    struct AttackCombo
    {
        public List<AttackState> attackCombo;
        public float coolTime;

        public AttackCombo(List<AttackState> attackCombo)
        {
            this.attackCombo = attackCombo;
            this.coolTime = 0;
        }
    }

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
        Vertical = 1,
        Horizontal,
        _360Low,
        Kick,
        Upercut,
        JumpAttack,
        DashAttack
    }

    [Header("State")]
    public BossState bossState;
    [SerializeField] AttackState attackState;

    float currentTime;
    int comboIndex = 0;

    BossHealth hpController;
    BossLocomotion locomotion;
    BossAnimationManager animationManager;
    BossStatus status;

    AttackCombo attackCombo1 = new AttackCombo(new List<AttackState>() { AttackState.Vertical, AttackState.Vertical });
    AttackCombo attackCombo2 = new AttackCombo(new List<AttackState>() { AttackState.Horizontal });
    AttackCombo attackCombo3 = new AttackCombo(new List<AttackState>() { AttackState.Horizontal, AttackState._360Low });
    AttackCombo attackCombo4 = new AttackCombo(new List<AttackState>() { AttackState.Vertical });
    AttackCombo attackCombo5 = new AttackCombo(new List<AttackState>() { AttackState.Horizontal, AttackState._360Low, AttackState.Upercut });
    AttackCombo attackCombo6 = new AttackCombo(new List<AttackState>() { AttackState.Upercut, AttackState._360Low });
    AttackCombo attackCombo7 = new AttackCombo(new List<AttackState>() { AttackState.Kick, AttackState.Upercut });

    AttackCombo[] combos;

    AttackCombo farAttackCombo1 = new AttackCombo(new List<AttackState>() { AttackState.DashAttack, AttackState.Horizontal, AttackState.Vertical });
    AttackCombo farAttackCombo2 = new AttackCombo(new List<AttackState>() { AttackState.DashAttack, AttackState.Horizontal });
    AttackCombo farAttackCombo3 = new AttackCombo(new List<AttackState>() { AttackState.DashAttack, AttackState.Vertical });
    AttackCombo farAttackCombo4 = new AttackCombo(new List<AttackState>() { AttackState.JumpAttack, AttackState.Vertical });
    AttackCombo farAttackCombo5 = new AttackCombo(new List<AttackState>() { AttackState.JumpAttack, AttackState.Upercut });

    AttackCombo[] farCombos;


    AttackCombo currentCombo;

    // Start is called before the first frame update
    void Start()
    {
        hpController = GetComponent<BossHealth>();
        locomotion = GetComponent<BossLocomotion>();
        animationManager = GetComponent<BossAnimationManager>();
        status = GetComponent<BossStatus>();
        enemyType = EnemyType.Boss;

        combos = new AttackCombo[] { attackCombo1, attackCombo2, attackCombo3, attackCombo4, attackCombo5, attackCombo6, attackCombo7 };
        farCombos = new AttackCombo[] { farAttackCombo1, farAttackCombo2, farAttackCombo3, farAttackCombo4, farAttackCombo5 };
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
        if (IsPlayerWakeBossUp(status.awakeDistance))
        {
            //상태를 일어난 상태로 바꾼다.
            print("sleep -> awake");
            bossState = BossState.Awake;

            //awake 애니메이션을 실행한다.
            animationManager.AwakeAnimationStart();

            //boss hp bar를 active 상태로 바꾼다
            UIManager.instance.ShowBossHpBar();
        }
    }

    // 이 함수는 플레이어가 일정 거리안에 들어오면 true를 반환해주는 함수이다.
    bool IsPlayerWakeBossUp(float distance)
    {
        Collider[] colliders =Physics.OverlapSphere(locomotion.myTransform.position,status.awakeDistance);

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
        if (animationManager.IsAwakeAnimationEnd())
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
        locomotion.SetMoveDirection(BossLocomotion.MoveType.Linear);
        locomotion.MoveBoss(BossLocomotion.MoveType.Linear);


        //내 몸의 방향을 플레이어를 향하도록 잡는다.
        locomotion.HandleRotation();

        //공격 대기 시간이 지나면, 공격을 한다.
        if (currentTime > status.idleTime)
        {
            //currentTime = 0;
            //SelectAttackCombo();
            //bossState = BossState.Attack;
            //print("Attack delay -> attack");

            comboIndex = 0;
            //현재 fsm을 attack으로 바꾼다.
            bossState = BossState.Attack;

            //콤보를 고르자
            currentCombo = SelectAttackCombo();

            //현재 attack fsm을 첫 번째 항목으로 바꾼다.
            attackState = currentCombo.attackCombo[comboIndex];

            if (attackState == AttackState.JumpAttack)
            {
                locomotion.SetMoveDirection(BossLocomotion.MoveType.Jump);
            }
            else if(attackState == AttackState.DashAttack)
            {
                locomotion.SetMoveDirection(BossLocomotion.MoveType.Dash);
            }

            //attack combo(list)에서 첫 번째 항목의 애니메이션을 재생한다.
            AttackAnimationStart(GetDistanceType((int)attackState), GetAttackType((int)attackState));
            print("distance Type : " + GetDistanceType((int)attackState) + "attack Type: " + GetAttackType((int)attackState));
        }
    }
    private void AttackAnimationStart(int distanceType, int attackType)
    {
        animationManager.AttackAnimationStart(distanceType, attackType);
    }
    // 이 함수는 어떤 공격을 할지 정해준다.
    // 정해진 콤보를 상태 함수로 만든다.
    private AttackCombo SelectAttackCombo()
    {
        locomotion.SetTargetPosition();
        locomotion.SetTargetDirection();
        //만약 거리가 가까우면, 가까운 거리 공격을 하자
        if (locomotion.targetDistance < status.attackDistance)
        {
            //int randNum = Random.Range(1, 6);
            //animationManager.AttackAnimationStart(1, randNum);
            //animationManager.TurnOnRootMotion();
            //attackState = (AttackState)randNum;
            int randNum = Random.Range(0, combos.Length);

            return combos[randNum];
        }
        //만약 거리가 멀면, 원거리 공격 콤보를 하자
        else
        {
            //int randNum = Random.Range(1, 3);
            //if (randNum == 1)
            //{
            //    attackState = AttackState.JumpAttack;
            //    animationManager.AttackAnimationStart(2, 1);
            //    locomotion.SetMoveDirection(BossLocomotion.MoveType.Jump);
                
            //}
            //else
            //{
            //    attackState = AttackState.DashAttack;
            //    animationManager.AttackAnimationStart(2, 0);
            //    locomotion.SetMoveDirection(BossLocomotion.MoveType.Dash);
            //}
            int randNum = Random.Range(0,farCombos.Length);
            return farCombos[randNum];
        }



        // 수정
        // attack combo 중에서 한 개를 고를 것임
        // attack combo 중에서 쿨타임이 없는 거 중에서 고를 것임
        // 골라지면 현재 
    }

    void Attack()
    {
        //공격 애니메이션이 끝나면, 공격대기시간으로 전환한다.

        if (attackState == AttackState.JumpAttack)
        {
            locomotion.MoveBoss(BossLocomotion.MoveType.Jump);
            if(locomotion.IsJumping() == false)
            {
                animationManager.SetTrigger("JumpDown");
                NextCombo();
            }

        }
        else if (attackState == AttackState.DashAttack)
        {
            locomotion.MoveBoss(BossLocomotion.MoveType.Dash);

            if(locomotion.IsDashing() == false)
            {
                animationManager.SetTrigger("RunToWalk");
                NextCombo();
            }
        }

        if (animationManager.IsAttackAnimationEnd())
        {
            NextCombo();
        }
    }

    void NextCombo()
    {
        comboIndex++;
        if (comboIndex >= currentCombo.attackCombo.Count)
        {
            print("attack -> attack delay");
            bossState = BossState.AttackDelay;
            animationManager.SetTrigger("AttackEnd");
            SelectAttackDelayMovement();
            currentTime = 0;
            comboIndex = 0;
        }
        else
        {
            attackState = currentCombo.attackCombo[comboIndex];
            AttackAnimationStart(GetDistanceType((int)attackState), GetAttackType((int)attackState));
            print("distance Type : " + GetDistanceType((int)attackState) + "attack Type: " + GetAttackType((int)attackState));
        }
    }

    int GetDistanceType(int state)
    {
        return (state - 1) / 5 + 1;
    }

    int GetAttackType(int state)
    {
        return state % 5;
    }
   
    public void Die()
    {
        //현재 내 hp가 55% 이하라면
        bossState = BossState.Die;
        //변신 애니메이션을 실행한다.
        animationManager.DeathAnimationStart();

        //이 스크립트를 파괴한다.
        this.enabled = false;
    }

    //거리를 기준으로 앞으로 움직일지 옆으로 움직일 지 선택하는 함수
    void SelectAttackDelayMovement()
    {
        //만약 플레이어랑의 거리가 멀다면
        //플레이어한테 다가오는 방향으로 온다
        //만약 플레이어랑 거리가 멀지 않다면,
        //플레이어 주위로 원을 하나 그린다음, 그 원의 특이한 지점을 잡는다

//        animationManager.TurnOffRootMotion();
        locomotion.SetMoveDirection(BossLocomotion.MoveType.Linear);
    }
 
}
