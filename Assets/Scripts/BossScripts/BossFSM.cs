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
        // Idle�� ����
        print("Idle�ϰ� �ִ� �����Դϴ�.");
        // �÷��̾ ������ �̵��Ѵ�
        if(Vector3.Distance(transform.position, target.transform.position) < moveDistance)
        {
            m_State = State.Move;
        }
    }

    void Move()
    {
        // Move����
        print("Move�ϰ� �ִ� �����Դϴ�");
        // ���� attack�Ÿ��� ������ ��������
        if(Vector3.Distance(transform.position, target.transform.position) < attackDistance)
        {
            m_State = State.Attack;
        }
    }

    void Attack()
    {
        // Attack ����
        print("Attack�ϰ� �ִ� �����Դϴ�");
        // ���� Move�Ÿ��� �־����� �ٽ� �̵�����
        if(Vector3.Distance(transform.position, target.transform.position) > attackDistance)
        {
            m_State = State.Move;
        }
    }

    void Damaged()
    {
        print("Damaged ���� �����Դϴ�");
    }

    void Die()
    {
        print("���� �����Դϴ�");
    }
}
