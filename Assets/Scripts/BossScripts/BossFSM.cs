using System.Collections;
using System.Collections.Generic;
using UnityEditor.Overlays;
using UnityEngine;

public class BossFSM : MonoBehaviour
{
    [SerializeField] float awakeDistance;
    [SerializeField] float attackDistance;
    [SerializeField] int moveType;
    public enum BossState
    {
        Sleep = 1,
        Awake = 2,
        AttackDelay = 4,
        Attack = 8,
        Die = 16
    }

    [SerializeField]BossState bossState;

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
        if (true)
        {
            
            bossState = BossState.AttackDelay;

            //���� �÷��̾���� �Ÿ��� �ִٸ�
            if (BossLocomotion.instance.targetDistance > attackDistance)
            {
                //�÷��̾����� �ٰ����� �������� �����Ѵ�.
                moveType = 0;
                print("awake -> linear move");
                
            }
            //���� �÷��̾�� �Ÿ��� ���� �ʴٸ�,
            else
            {
                //�÷��̾� ��ǥ�� ���� �߽����� ���, ������ ���ϸ鼭 �����δ�.
                moveType = 1;
                print("awake -> circle move");
            }
        }
    }

    void AttackDelay()
    {
        //���� ��� �ð�

        //�̵� ��ǥ�� ���ؼ� ���ݾ� �̵��Ѵ�.
        if (moveType == 0)
        {
            BossLocomotion.instance.LinearMovement(BossStatus.moveSpeed);
        }
        else if (moveType == 1)
        {
            BossLocomotion.instance.CircleMovement(BossStatus.moveSpeed);   
        }

        //�� ���� ������ �÷��̾ ���ϵ��� ��´�.
        BossLocomotion.instance.HandleRotation();

        //���� ��� �ð��� ������, ������ �Ѵ�.

        //���� ���� �� ���� ������ ���� �ʹ�.
        //���� ���� Ŭ������ �߿��� Ÿ�̸� �ð��� 0���� ���� �޺���� ����Ʈ�� �����.
        //�� �߿��� ���� ���ڸ� �̾Ƽ� ���� ���� Ŭ������ �޺� ����Ʈ�� �޾ƿ´�.
        //�޺� ����Ʈ�� ���� �ִ� enum�� ���� �Լ��� �����Ѵ�.
    }

    void Attack()
    {
        //���ǿ� ���� ������ �ߵ��Ѵ�.

        //���� �ִϸ��̼��� ������, ���ݴ��ð����� ��ȯ�Ѵ�.

        //���� �÷��̾���� �Ÿ��� �ִٸ�
        //�÷��̾����� �ٰ����� �������� �´�
        //���� �÷��̾�� �Ÿ��� ���� �ʴٸ�,
        //�÷��̾� ������ ���� �ϳ� �׸�����, �� ���� Ư���� ������ ��´�
    }

    void Die()
    {
        //���� �� hp�� 55% ���϶��

        //���� �ִϸ��̼��� �����Ѵ�.
        //FSM�� 2������ FSM���� �ٲ۴�.
    }

    void TakeDamage(float damage)
    {

    }
}
