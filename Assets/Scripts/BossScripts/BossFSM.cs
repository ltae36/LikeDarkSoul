using System.Collections;
using System.Collections.Generic;
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
    BossHealth hpController;

    // Start is called before the first frame update
    void Start()
    {
        bossState = BossState.Sleep;
        hpController = GetComponent<BossHealth>();
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
        if (IsPlayerWakeBossUp(awakeDistance))
        {
            //���¸� �Ͼ ���·� �ٲ۴�.
            print("sleep -> awake");
            bossState = BossState.Awake;

            //awake �ִϸ��̼��� �����Ѵ�.
            BossAnimationManager.instance.AwakeAnimationStart();

            //boss hp bar�� active ���·� �ٲ۴�
            UIManager.instance.ShowBossHpBar();
        }
    }

    // �� �Լ��� �÷��̾ ���� �Ÿ��ȿ� ������ true�� ��ȯ���ִ� �Լ��̴�.
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
        //�Ͼ�� ����

        //���� hp�ٸ� Ȱ��ȭ��Ų��.
        //���� �ݶ��̴��� Ȱ��ȭ��Ų��.
        //�ִϸ��̼��� ������, ���� ������ ���·� ��ȯ�Ѵ�. 
        if (BossAnimationManager.instance.IsAwakeAnimationEnd())
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
        BossLocomotion.instance.SetMoveDirection(BossLocomotion.MoveType.Linear);
        BossLocomotion.instance.MoveBoss(BossLocomotion.MoveType.Linear);


        //�� ���� ������ �÷��̾ ���ϵ��� ��´�.
        BossLocomotion.instance.HandleRotation();

        //���� ��� �ð��� ������, ������ �Ѵ�.
        if (currentTime > idleTime)
        {
            currentTime = 0;
            SelectAttackCombo();
            bossState = BossState.Attack;
            print("Attack delay -> attack");
        }
    }
    
    // �� �Լ��� � ������ ���� �����ش�.
    // ������ �޺��� ���� �Լ��� �����.
    private void SelectAttackCombo()
    {
        BossLocomotion.instance.SetTargetPosition();
        BossLocomotion.instance.SetTargetDirection();
        //���� �Ÿ��� ������, horizontal �Ǵ� vertical ������ �Ѵ�.
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
        //���� �Ÿ��� �ָ�, jump attack �̳� dash attack�� �Ѵ�.
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
        //���ǿ� ���� ������ �ߵ��Ѵ�.
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
        //���� �ִϸ��̼��� ������, ���ݴ��ð����� ��ȯ�Ѵ�.

    }

    void Horizontal()
    {
        //horizontal ������ �Ѵ�.
        //print("Horizontal");
        if(BossAnimationManager.instance.IsAttackAnimationEnd(1,1))
        {
            bossState = BossState.AttackDelay;

            //���� �÷��̾���� �Ÿ��� �ִٸ�
            //�÷��̾����� �ٰ����� �������� �����Ѵ�.
            print("attack -> linear move");
            SelectAttackDelayMovement();
        }
    }
    void Vertical()
    {
        //Vertical ������ �Ѵ�.
        //print("Vertical");
        if (BossAnimationManager.instance.IsAttackAnimationEnd(1, 0))
        {
            bossState = BossState.AttackDelay;

            //���� �÷��̾���� �Ÿ��� �ִٸ�
            //�÷��̾����� �ٰ����� �������� �����Ѵ�.
            print("attack -> linear move");
            SelectAttackDelayMovement();
        }
    }

    void JumpAttack()
    {
        //jump attack ������ �Ѵ�.
        //print("JumpAttack");
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
        //dash attack ������ �Ѵ�.
        //print("DashAttack");
        BossLocomotion.instance.MoveBoss(BossLocomotion.MoveType.Dash);
        if (BossLocomotion.instance.IsDashing() == false)
        {
            bossState = BossState.AttackDelay;
            BossAnimationManager.instance.SetTrigger("RunToWalk");
            print("attack -> linear move");
            SelectAttackDelayMovement();
        }
    }
    public void Die()
    {
        //���� �� hp�� 55% ���϶��
        bossState = BossState.Die;
        //���� �ִϸ��̼��� �����Ѵ�.

        BossAnimationManager.instance.DeathAnimationStart();
        //FSM�� 2������ FSM���� �ٲ۴�.

        //�� ��ũ��Ʈ�� �ı��Ѵ�.
        this.enabled = false;
    }

    //�ִϸ��̼��� �������� Ȯ���ϴ� �Լ�
    //���߿� �Ű������� ���� ���̰�, �ƴϸ� bossanimationmanager�� ���� ��������


    //�Ÿ��� �������� ������ �������� ������ ������ �� �����ϴ� �Լ�
    void SelectAttackDelayMovement()
    {
        //���� �÷��̾���� �Ÿ��� �ִٸ�
        //�÷��̾����� �ٰ����� �������� �´�
        //���� �÷��̾�� �Ÿ��� ���� �ʴٸ�,
        //�÷��̾� ������ ���� �ϳ� �׸�����, �� ���� Ư���� ������ ��´�
        BossLocomotion.instance.SetMoveDirection(BossLocomotion.MoveType.Linear);
    }

    
}
