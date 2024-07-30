using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class IdleState : State
{
    public AttackState attackState;
    public FarAttack farAttackState;
    public DashAttack dashAttackState;

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


            // Ÿ�ٰ��� �Ÿ��� �޾ƿ���
            float distance = BossLocomotion.instance.targetDistance;

            //�ٰŸ� ����
            if (distance < nearAttackRange)
            {

            }
            //�߰Ÿ�����
            else if (distance < normalAttackRange)
            {

            }
            //���Ÿ� ����
            else if (distance < farAttackRange)
            {
                dashAttackState.SetDashPosition(BossLocomotion.instance.target.transform.position);
                return dashAttackState;

            }
        }
        currentTime += Time.deltaTime;

        //print(currentTime);
        //���� Ÿ�̸� �ð��� �� �Ǹ�
        BossLocomotion.instance.MoveBoss();
        //��������.
        return this;
    }

}
