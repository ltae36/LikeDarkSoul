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
    BossHealthBarController healthBar;

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
        healthBar = GetComponent<BossHealthBarController>();

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
        //�ڰ� �ִ� ����

        //���� �÷��̾ Į�� �̰ų�

        //���� �÷��̾ Į�� �̾Ҵ� ���, ���� �Ÿ� ���� ������,
        if (IsPlayerWakeBossUp(status.awakeDistance))
        {
            //���¸� �Ͼ ���·� �ٲ۴�.
            print("sleep -> awake");
            bossState = BossState.Awake;

            //awake �ִϸ��̼��� �����Ѵ�.
            animationManager.AwakeAnimationStart();

            //boss hp bar�� active ���·� �ٲ۴�
            healthBar.ShowBossHpBar();
        }
    }

    // �� �Լ��� �÷��̾ ���� �Ÿ��ȿ� ������ true�� ��ȯ���ִ� �Լ��̴�.
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
        //�Ͼ�� ����

        //���� hp�ٸ� Ȱ��ȭ��Ų��.
        //���� �ݶ��̴��� Ȱ��ȭ��Ų��.
        //�ִϸ��̼��� ������, ���� ������ ���·� ��ȯ�Ѵ�. 
        if (animationManager.IsAwakeAnimationEnd())
        {
            
            bossState = BossState.AttackDelay;

            //���� �÷��̾���� �Ÿ��� �ִٸ�
            //�÷��̾����� �ٰ����� �������� �����Ѵ�.
            print("awake -> linear move");
            SelectAttackDelayMovement();
            //���� �÷��̾�� �Ÿ��� ���� �ʴٸ�,
            //�÷��̾� ��ǥ�� ���� �߽����� ���, ������ ���ϸ鼭 �����δ�.
        }
    }

    void AttackDelay()
    {
        //���� ��� �ð�
        currentTime += Time.deltaTime;
        //�̵� ��ǥ�� ���ؼ� ���ݾ� �̵��Ѵ�.
        locomotion.SetMoveDirection(BossLocomotion.MoveType.Linear);
        locomotion.MoveBoss(BossLocomotion.MoveType.Linear);


        //�� ���� ������ �÷��̾ ���ϵ��� ��´�.
        locomotion.HandleRotation();

        //���� ��� �ð��� ������, ������ �Ѵ�.
        if (currentTime > status.idleTime)
        {
            //currentTime = 0;
            //SelectAttackCombo();
            //bossState = BossState.Attack;
            //print("Attack delay -> attack");

            comboIndex = 0;
            //���� fsm�� attack���� �ٲ۴�.
            bossState = BossState.Attack;

            //�޺��� ����
            currentCombo = SelectAttackCombo();

            //���� attack fsm�� ù ��° �׸����� �ٲ۴�.
            attackState = currentCombo.attackCombo[comboIndex];

            if (attackState == AttackState.JumpAttack)
            {
                locomotion.SetMoveDirection(BossLocomotion.MoveType.Jump);
            }
            else if(attackState == AttackState.DashAttack)
            {
                locomotion.SetMoveDirection(BossLocomotion.MoveType.Dash);
            }

            //attack combo(list)���� ù ��° �׸��� �ִϸ��̼��� ����Ѵ�.
            AttackAnimationStart(GetDistanceType((int)attackState), GetAttackType((int)attackState));
            print("distance Type : " + GetDistanceType((int)attackState) + "attack Type: " + GetAttackType((int)attackState));
        }
    }
    private void AttackAnimationStart(int distanceType, int attackType)
    {
        animationManager.AttackAnimationStart(distanceType, attackType);
    }
    // �� �Լ��� � ������ ���� �����ش�.
    // ������ �޺��� ���� �Լ��� �����.
    private AttackCombo SelectAttackCombo()
    {
        locomotion.SetTargetPosition();
        locomotion.SetTargetDirection();
        //���� �Ÿ��� ������, ����� �Ÿ� ������ ����
        if (locomotion.targetDistance < status.attackDistance)
        {
            //int randNum = Random.Range(1, 6);
            //animationManager.AttackAnimationStart(1, randNum);
            //animationManager.TurnOnRootMotion();
            //attackState = (AttackState)randNum;
            int randNum = Random.Range(0, combos.Length);

            return combos[randNum];
        }
        //���� �Ÿ��� �ָ�, ���Ÿ� ���� �޺��� ����
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



        // ����
        // attack combo �߿��� �� ���� �� ����
        // attack combo �߿��� ��Ÿ���� ���� �� �߿��� �� ����
        // ������� ���� 
    }

    void Attack()
    {
        //���� �ִϸ��̼��� ������, ���ݴ��ð����� ��ȯ�Ѵ�.

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
        //���� �� hp�� 55% ���϶��
        bossState = BossState.Die;
        //���� �ִϸ��̼��� �����Ѵ�.
        animationManager.DeathAnimationStart();
        
        //hp bar�� �����.
        healthBar.HideBossHpBar();

        //�� ��ũ��Ʈ�� �ı��Ѵ�.
        this.enabled = false;
    }

    //�Ÿ��� �������� ������ �������� ������ ������ �� �����ϴ� �Լ�
    void SelectAttackDelayMovement()
    {
        //���� �÷��̾���� �Ÿ��� �ִٸ�
        //�÷��̾����� �ٰ����� �������� �´�
        //���� �÷��̾�� �Ÿ��� ���� �ʴٸ�,
        //�÷��̾� ������ ���� �ϳ� �׸�����, �� ���� Ư���� ������ ��´�

//        animationManager.TurnOffRootMotion();
        locomotion.SetMoveDirection(BossLocomotion.MoveType.Linear);
    }


}
