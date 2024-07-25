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
        //���� �÷��̾ ���ݹ����� �������� üũ
        //���� ������ ������
        //�÷��̾ ã�Ҵٴ� ��� �����ְ�
        //�÷��̾ ���ؼ� �����̱�
        
    }

    void Move()
    {
        //�÷��̾����� �ٰ����� (�پ �ٰ����� ��쵵 �ְ� �ɾ �ٰ����� ��쵵 �ִ�)
        //1�� ������ �ȴٰ� ���� ������ �ʹ� �ָ� �پ �´�?
        //���� ������ ���� ���� ������ �����ϱ�
    }

    void Attack()
    {
        //�÷��̾ �����Ѵ�.
        //������ ���� ���� �÷��̾ ������ ��� �����Ѵ�.
        
    }

    void Damaged()
    {

    }

    void Die()
    {

    }
}
