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
        //자고 있는 상태

        //만약 플레이어가 칼을 뽑거나

        //만일 플러이어가 칼을 뽑았는 경우, 일정 거리 내로 들어오면,
        if (IsPlayerWakeBossUp(awakeDistance))
        {
            //상태를 일어난 상태로 바꾼다.
            print("sleep -> awake");
            bossState = BossState.Awake;        
        }
    }

    // 이 함수는 플레이어가 일정 거리안에 들어오면 true를 반환해주는 함수이다.
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
        //일어나는 상태

        //보스 hp바를 활성화시킨다.
        //보스 콜라이더를 활성화시킨다.
        //애니메이션이 끝나면, 공격 딜레이 상태로 전환한다. 
        if (true)
        {
            
            bossState = BossState.AttackDelay;

            //만약 플레이어랑의 거리가 멀다면
            if (BossLocomotion.instance.targetDistance > attackDistance)
            {
                //플레이어한테 다가오는 방향으로 설정한다.
                moveType = 0;
                print("awake -> linear move");
                
            }
            //만약 플레이어랑 거리가 멀지 않다면,
            else
            {
                //플레이어 좌표를 원의 중심으로 잡고, 방향을 구하면서 움직인다.
                moveType = 1;
                print("awake -> circle move");
            }
        }
    }

    void AttackDelay()
    {
        //공격 대기 시간

        //이동 목표를 향해서 조금씩 이동한다.
        if (moveType == 0)
        {
            BossLocomotion.instance.LinearMovement(BossStatus.moveSpeed);
        }
        else if (moveType == 1)
        {
            BossLocomotion.instance.CircleMovement(BossStatus.moveSpeed);   
        }

        //내 몸의 방향을 플레이어를 향하도록 잡는다.
        BossLocomotion.instance.HandleRotation();

        //공격 대기 시간이 지나면, 공격을 한다.

        //내가 지금 할 공격 패턴을 고르고 싶다.
        //공격 패턴 클래스를 중에서 타이머 시간이 0보다 작은 콤보들로 리스트를 만든다.
        //그 중에서 랜덤 숫자를 뽑아서 공격 패턴 클래스의 콤보 리스트를 받아온다.
        //콤보 리스트에 적혀 있는 enum에 따라서 함수를 실행한다.
    }

    void Attack()
    {
        //조건에 따라 공격을 발동한다.

        //공격 애니메이션이 끝나면, 공격대기시간으로 전환한다.

        //만약 플레이어랑의 거리가 멀다면
        //플레이어한테 다가오는 방향으로 온다
        //만약 플레이어랑 거리가 멀지 않다면,
        //플레이어 주위로 원을 하나 그린다음, 그 원의 특이한 지점을 잡는다
    }

    void Die()
    {
        //현재 내 hp가 55% 이하라면

        //변신 애니메이션을 실행한다.
        //FSM을 2페이즈 FSM으로 바꾼다.
    }

    void TakeDamage(float damage)
    {

    }
}
