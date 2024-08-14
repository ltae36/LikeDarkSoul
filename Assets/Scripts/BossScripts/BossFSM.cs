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
        Vertical = 1,
        Horizontal,
        _360Low,
        Kick,
        Upercut,
        JumpAttack,
        DashAttack
    }
    [Header("Distance")]
    [SerializeField] float awakeDistance =20;
    [SerializeField] float attackDistance = 6;

    [Header("Time")]
    [SerializeField] float idleTime =3;

    [Header("State")]
    [SerializeField]BossState bossState;
    [SerializeField] AttackState attackState;

    float currentTime;
    BossHealth hpController;
    BossLocomotion locomotion;
    BossAnimationManager animationManager;

    // Start is called before the first frame update
    void Start()
    {
        hpController = GetComponent<BossHealth>();
        locomotion = GetComponent<BossLocomotion>();
        animationManager = GetComponent<BossAnimationManager>();
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
            animationManager.AwakeAnimationStart();

            //boss hp bar�� active ���·� �ٲ۴�
            UIManager.instance.ShowBossHpBar();
        }
    }

    // �� �Լ��� �÷��̾ ���� �Ÿ��ȿ� ������ true�� ��ȯ���ִ� �Լ��̴�.
    bool IsPlayerWakeBossUp(float distance)
    {
        Collider[] colliders =Physics.OverlapSphere(locomotion.myTransform.position,awakeDistance);

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
        locomotion.SetTargetPosition();
        locomotion.SetTargetDirection();
        //���� �Ÿ��� ������, horizontal �Ǵ� vertical ������ �Ѵ�.
        //print(locomotion.targetDistance);
        if (locomotion.targetDistance < attackDistance)
        {
            int randNum = Random.Range(1, 6);
            animationManager.AttackAnimationStart(1, randNum);
            animationManager.TurnOnRootMotion();
            attackState = (AttackState)randNum;
        }
        //���� �Ÿ��� �ָ�, jump attack �̳� dash attack�� �Ѵ�.
        else
        {
            int randNum = Random.Range(1, 3);
            if (randNum == 1)
            {
                attackState = AttackState.JumpAttack;
                animationManager.AttackAnimationStart(2, 1);
                locomotion.SetMoveDirection(BossLocomotion.MoveType.Jump);
                
            }
            else
            {
                attackState = AttackState.DashAttack;
                animationManager.AttackAnimationStart(2, 0);
                locomotion.SetMoveDirection(BossLocomotion.MoveType.Dash);
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
            case AttackState._360Low:
                _360Low();
                break;
            case AttackState.Kick:
                Kick();
                break;
            case AttackState.Upercut:
                Upercut();
                break;
            case AttackState.JumpAttack:
                JumpAttack();
                break;
            case AttackState.DashAttack:
                JumpAttack();
                break;
        }
        //���� �ִϸ��̼��� ������, ���ݴ��ð����� ��ȯ�Ѵ�.

    }

    void Horizontal()
    {
        //horizontal ������ �Ѵ�.
        //print("Horizontal");
        if(animationManager.IsAttackAnimationEnd())
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
        if (animationManager.IsAttackAnimationEnd())
        {
            bossState = BossState.AttackDelay;

            //���� �÷��̾���� �Ÿ��� �ִٸ�
            //�÷��̾����� �ٰ����� �������� �����Ѵ�.
            print("attack -> linear move");
            SelectAttackDelayMovement();
        }
    }

    void _360Low()
    {
        if (animationManager.IsAttackAnimationEnd())
        {
            bossState = BossState.AttackDelay;

            //���� �÷��̾���� �Ÿ��� �ִٸ�
            //�÷��̾����� �ٰ����� �������� �����Ѵ�.
            print("attack -> linear move");
            SelectAttackDelayMovement();
        }
    }

    void Kick()
    {
        if (animationManager.IsAttackAnimationEnd())
        {
            bossState = BossState.AttackDelay;

            //���� �÷��̾���� �Ÿ��� �ִٸ�
            //�÷��̾����� �ٰ����� �������� �����Ѵ�.
            print("attack -> linear move");
            SelectAttackDelayMovement();
        }
    }
    void Upercut()
    {
        if (animationManager.IsAttackAnimationEnd())
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
        locomotion.MoveBoss(BossLocomotion.MoveType.Jump);
       
        if(locomotion.IsJumping() == false)
        {
            bossState = BossState.AttackDelay;
            animationManager.SetTrigger("JumpDown");
            transform.position = locomotion.targetPosition;
            print("attack -> linear move");
            SelectAttackDelayMovement();
        }
    }

    void DashAttack()
    {
        //dash attack ������ �Ѵ�.
        //print("DashAttack");
        locomotion.MoveBoss(BossLocomotion.MoveType.Dash);
        if (locomotion.IsDashing() == false)
        {
            bossState = BossState.AttackDelay;
            animationManager.SetTrigger("RunToWalk");
            print("attack -> linear move");
            SelectAttackDelayMovement();
        }
    }
    public void Die()
    {
        //���� �� hp�� 55% ���϶��
        bossState = BossState.Die;
        //���� �ִϸ��̼��� �����Ѵ�.
        animationManager.DeathAnimationStart();

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
