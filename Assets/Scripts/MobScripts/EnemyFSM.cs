using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM : MonoBehaviour
{
    public enum EnemyState 
    {
        non,
        revival,
        idle,
        chase,
        attack,
        hit,
        death
    }

    public EnemyState undeadState = EnemyState.non;

    void Start()
    {
        
    }

    void Update()
    {
        switch (undeadState) 
        {
            case EnemyState.non:
                break;
            case EnemyState.revival:
                revival();
                break;
            case EnemyState.idle:
                break;
            case EnemyState.chase:
                chase();
                break;
            case EnemyState.attack:
                attack();
                break;
            case EnemyState.hit:
                hitted();
                break;
            case EnemyState.death:
                death();
                break;
        }
    }

    private void death()
    {
        // hp�� 0�� �Ǹ� ��� �ִϸ��̼��� ����ǰ� ���׵� ���°� �ȴ�.
        // ������Ʈ�� ��Ȱ��ȭ �ȴ�.
    }

    private void hitted()
    {
        // �ǰ� �ִϸ��̼��� ����ȴ�.
        // hp�� �����Ѵ�.
    }

    private void chase()
    {
        // �÷��̾ �Ѿư��� (NavMeshAgent)
        // �÷��̾ ���� ���� �ȿ� ������ attack ���� ��ȯ
        // �÷��̾ �þ߿��� ������� idle ���� ��ȯ
    }

    private void attack()
    {
        // ���� �ִϸ��̼��� ����Ѵ�.
        // �÷��̾��� �Ÿ��� �־����� chase ���� ��ȯ
        // ������ ������ hit���� ��ȯ
    }

    private void revival()
    {
        // �÷��̾ ���� ���� �ȿ� ������ �Ͼ�� �ִϸ��̼��� ���
        // �ִϸ��̼� ����� ������ �÷��̾ ���� �ٶ󺻴�
        // attack ���� ��ȯ
    }
}
