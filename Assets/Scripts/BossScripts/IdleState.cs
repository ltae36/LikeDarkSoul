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
            //공격을 할 것임
            //공격을 할 건데 공격 타입을 정할 것임
            //공격 타입은 지금 플레이어의 위치에 따라 결정 된다.
            //그 외에는 랜덤으로 지정할 것이다.
            //공격 패턴은 다음과 같다
            //근거리: 잡기, 철권, 철사장 (근거리는 보스의 위치가 변하지 않는다)
            //일반거리: 찌르기, 횡베기, naereo zzigi (일반거리 공격을 하면 보스는 플레이어 가까이 다가가게 된다 이 때 스텝은 애니메이션의 스텝을 따르기로 하자)
            //원거리: 점프, 점프후 내려찍기 (원거리 공격은 대쉬와 점프를 동반한다. 원거리 공격 후 일반 거리 공격 콤보가 들어가기도 한다.)


            // 타겟과의 거리를 받아오자
            float distance = BossLocomotion.instance.targetDistance;

            //근거리 공격
            if (distance < nearAttackRange)
            {

            }
            //중거리공격
            else if (distance < normalAttackRange)
            {

            }
            //원거리 공격
            else if (distance < farAttackRange)
            {
                dashAttackState.SetDashPosition(BossLocomotion.instance.target.transform.position);
                return dashAttackState;

            }
        }
        currentTime += Time.deltaTime;

        //print(currentTime);
        //만약 타이머 시간이 다 되면
        BossLocomotion.instance.MoveBoss();
        //움직이자.
        return this;
    }

}
