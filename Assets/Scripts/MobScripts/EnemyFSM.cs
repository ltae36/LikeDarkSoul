using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM : MonoBehaviour
{
    public enum EnemyState 
    {
        Sleep,
        Awake,
        Idle,
        Run,
        Attack,
        AttackDelay,
        Hit,
        Die
    }

    public EnemyState undeadState;

    void Start()
    {
        
    }

    void Update()
    {
        switch (undeadState) 
        {
            case EnemyState.Sleep:
                break;
            case EnemyState.Awake:
                break;
            case EnemyState.Idle:
                break;
            case EnemyState.Run:
                break;
            case EnemyState.Attack:        
                break;
            case EnemyState.AttackDelay:
                break;
            case EnemyState.Hit:
                break;
            case EnemyState.Die:
                break;

        }
    }

    private void Sleep()
    {
        //�ڰ� �ִ� ����
        //���� �÷��̾ ���� �Ÿ� ���� ������...
        //���¸� awake�� �ٲ۴�.
        //�ִϸ��̼��� �����Ѵ�.
    }

    private void Awake()
    {
        //�Ͼ�� ����
        //���� awake �ִϸ��̼��� ������...
        //�÷��̾���� �Ÿ��� ����
        //������ ����, �����̸� �Ȱ�, �ָ� ����.
    }
    private void Idle()
    {
        //�ɾ player���� �ٰ����� ����

        //�÷��̾ ���ؼ� �ɾ�´�.
        //���� �Ÿ��� ���� �Ÿ����� �۾�����
        //�����Ѵ�.
        //���� �Ÿ��� ���� �Ÿ����� �ָ�
        //�޸���.
    }
    private void Chase()
    {
        // �ھ player���� �ٰ����� ����

        //�÷��̾ ���ؼ� �پ�´�.
        //���� �Ÿ��� ���� �Ÿ����� �۾�����
        //�����Ѵ�.
    }
    private void Attack()
    {
        // �����ϴ� ����

        //���ݾִϸ��̼��� ������, ���� ��� ���·� �Ѿ��.
    }

    private void AttackDelay()
    {

    }
    private void Hitted()
    {
        // �ǰ� �ִϸ��̼��� ����ȴ�.
        // hp�� �����Ѵ�.
    }

    private void Death()
    {
        // hp�� 0�� �Ǹ� ��� �ִϸ��̼��� ����ǰ� ���׵� ���°� �ȴ�.
        // ������Ʈ�� ��Ȱ��ȭ �ȴ�.
    }
}
