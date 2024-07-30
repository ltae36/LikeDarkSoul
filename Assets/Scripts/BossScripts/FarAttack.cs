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
        //현재 점프 이동 방향을 구한다.
        Vector3 jumpDirection = direction.normalized * jumpVelocity + Vector3.up * jumpVelocity + Physics.gravity * Mathf.Pow(currentTime, 2.0f) / 2;
        //transform을 변화시킨다.
        BossLocomotion.instance.transform.position += jumpDirection * Time.deltaTime;
        //시간을 더해준다.
        currentTime += Time.deltaTime;

        //만약 도착하면, idlestate로 바꾼다.
        print(Vector3.Distance(target, BossLocomotion.instance.myTransform.position));
        if (Vector3.Distance(target, BossLocomotion.instance.myTransform.position) < 1.1f)
        {
            BossLocomotion.instance.SetIdleDirection();
            return idleState;
        }
        return this;


    }

    // 이 클래스가 호출되기 전에 실행되어야 하는 함수
    public void SetJumpVelocity()
    {
        //이동 속도가 필요하다

        //각 순간의 이동위치는 다음 식을 따른다
        // x = vt
        // y =  vt - 1/2 g*t*t

        // 우리 최종 목표는 target이다
        // v를 구할 수 있다.
        // 2v*v/g = distance -> v를 구할 수 있다.
        target = BossLocomotion.instance.target.transform.position;
        direction = target - transform.position;
        direction.y = 0;

        jumpVelocity = Mathf.Sqrt(direction.magnitude * Physics.gravity.magnitude / 2);
        currentTime = 0;

        print("jump Velocity : " + jumpVelocity);
    }
    

}
