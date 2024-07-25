using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    Idle,
    Move,
    Attack,
    Damaged,
    Die
}
public class BossFSM : MonoBehaviour
{
    public GameObject target;
    public float attackDistance;
    public float moveDistance;


    State m_State;
    // Start is called before the first frame update
    void Start()
    {
        m_State = State.Idle;
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (m_State)
        {
            case State.Idle:
                break;
            case State.Move:
                break;
            case State.Attack:
                break;
            case State.Damaged:
                break;
            case State.Die:
                break;
        }
    }

    void Idle()
    {
        // Idle한 상태
        print("Idle하고 있는 상태입니다.");
        // 플레이어가 들어오면 이동한다
        if(Vector3.Distance(transform.position, target.transform.position) < moveDistance)
        {
            m_State = State.Move;
        }
    }

    void Move()
    {
        // Move상태
        print("Move하고 있는 상태입니다");
        // 만약 attack거리로 들어오면 공격하자
        if(Vector3.Distance(transform.position, target.transform.position) < attackDistance)
        {
            m_State = State.Attack;
        }
    }

    void Attack()
    {
        // Attack 상태
        print("Attack하고 있는 상태입니다");
        // 만약 Move거리로 멀어지면 다시 이동하자
        if(Vector3.Distance(transform.position, target.transform.position) > attackDistance)
        {
            m_State = State.Move;
        }
    }

    void Damaged()
    {
        print("Damaged 받은 상태입니다");
    }

    void Die()
    {
        print("죽은 상태입니다");
    }
}
