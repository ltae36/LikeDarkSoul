using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class IdleState : State
{
    public NearAttack nearAttackState;
    public NormalAttack normalAttackState;
    public FarAttack farAttackState;


    [SerializeField] float idleTime = 2.0f;
    float currentTime = 0;
    float nearAttackRange;
    float normalAttackRange;
    float farAttackRange;


    private void Start()
    {
        nearAttackRange = BossAttack.instance.nearAttackRange;
        normalAttackRange = BossAttack.instance.normalAttackRange;
        farAttackRange = BossAttack.instance.farAttackRange;

    }
    public override State RunCurrentState()
    {
        if (currentTime > idleTime)
        {
            //������ �� ����
            //������ �� �ǵ� ���� Ÿ���� ���� ����
            //���� Ÿ���� ���� �÷��̾��� ��ġ�� ���� ���� �ȴ�.
            //�� �ܿ��� �������� ������ ���̴�.
            //���� ������ ������ ����
            //�ٰŸ�: ���, ö��, ö���� (�ٰŸ��� ������ ��ġ�� ������ �ʴ´�)
            //�ϹݰŸ�: ���, Ⱦ����, naereo zzigi (�ϹݰŸ� ������ �ϸ� ������ �÷��̾� ������ �ٰ����� �ȴ� �� �� ������ �ִϸ��̼��� ������ ������� ����)
            //���Ÿ�: ����, ������ ������� (���Ÿ� ������ �뽬�� ������ �����Ѵ�. ���Ÿ� ���� �� �Ϲ� �Ÿ� ���� �޺��� ���⵵ �Ѵ�.)

            currentTime = 0;
            // Ÿ�ٰ��� �Ÿ��� �޾ƿ���
            float distance = BossLocomotion.instance.targetDistance;
            print(distance);

            //�ٰŸ� ����
            if (distance < nearAttackRange)
            {
                return nearAttackState;
            }
            //�߰Ÿ�����
            else if (distance < normalAttackRange)
            {
                return normalAttackState;
            }
            //���Ÿ� ����
            else if (distance < farAttackRange)
            {
                return farAttackState;

            }
            
        }
        currentTime += Time.deltaTime;

        //print(currentTime);
        
        //���� Ÿ�̸� �ð��� �� �Ǹ�
        BossLocomotion.instance.MoveBoss(BossLocomotion.MoveType.Linear);
        //��������.
        return this;
    }

}
