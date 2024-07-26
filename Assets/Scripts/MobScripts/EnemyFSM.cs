using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM : MonoBehaviour
{
    enum EnemyState
    {
        Idle,
        Find,
        Move,
        Attack,
        Damaged,
        Die
    }

    EnemyState m_State;

    [SerializeField] float findDistance;

    Transform player;

    // Start is called before the first frame update
    void Start()
    {
        m_State = EnemyState.Idle; 
    }

    // Update is called once per frame
    void Update()
    {
        switch (m_State)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Move:
                Move();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Damaged:
                Damaged();
                break;
            case EnemyState.Die:
                Die();
                break;
        }
    }

    void Idle()
    {
        //만약 플레이어가 공격범위로 들어오는지 체크
        //공격 범위로 들어오면
        //플레이어를 찾았다는 모습 보여주고
        //플레이어를 향해서 움직이기
        
    }

    void Move()
    {
        //플레이어한테 다가가기 (뛰어서 다가가는 경우도 있고 걸어서 다가가는 경우도 있다)
        //1초 정도는 걷다가 만약 상대방이 너무 멀면 뛰어서 온다?
        //만약 정해진 범위 내로 들어오면 공격하기
    }

    void Attack()
    {
        //플레이어를 공격한다.
        //정해진 범위 내에 플레이어가 있으면 계속 공격한다.
        
    }

    void Damaged()
    {

    }

    void Die()
    {

    }
}
