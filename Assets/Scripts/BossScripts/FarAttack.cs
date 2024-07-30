using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FarAttack : State
{
    public State idleState;

    [SerializeField]
    float jumpVelocity;
    float currentTime = 0;

    Vector3 direction;
    Vector3 target;
    public override State RunCurrentState()
    {
        //���� ���� �̵� ������ ���Ѵ�.
        Vector3 jumpDirection = direction.normalized * jumpVelocity + Vector3.up * jumpVelocity + Physics.gravity * Mathf.Pow(currentTime, 2.0f) / 2;
        //transform�� ��ȭ��Ų��.
        BossLocomotion.instance.transform.position += jumpDirection * Time.deltaTime;
        //�ð��� �����ش�.
        currentTime += Time.deltaTime;

        //���� �����ϸ�, idlestate�� �ٲ۴�.
        print(Vector3.Distance(target, BossLocomotion.instance.myTransform.position));
        if (Vector3.Distance(target, BossLocomotion.instance.myTransform.position) < 1.1f)
        {
            BossLocomotion.instance.SetIdleDirection();
            return idleState;
        }
        return this;


    }

    // �� Ŭ������ ȣ��Ǳ� ���� ����Ǿ�� �ϴ� �Լ�
    public void SetJumpVelocity()
    {
        //�̵� �ӵ��� �ʿ��ϴ�

        //�� ������ �̵���ġ�� ���� ���� ������
        // x = vt
        // y =  vt - 1/2 g*t*t

        // �츮 ���� ��ǥ�� target�̴�
        // v�� ���� �� �ִ�.
        // 2v*v/g = distance -> v�� ���� �� �ִ�.
        target = BossLocomotion.instance.target.transform.position;
        direction = target - transform.position;
        direction.y = 0;

        jumpVelocity = Mathf.Sqrt(direction.magnitude * Physics.gravity.magnitude / 2);
        currentTime = 0;

        print("jump Velocity : " + jumpVelocity);
    }
    

}
