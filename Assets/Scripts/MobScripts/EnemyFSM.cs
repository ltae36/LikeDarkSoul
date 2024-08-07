using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM : MonoBehaviour
{
    public enum EnemyState 
    {
        dead,
        stand,
        idle,
        chase,
        attack,
        hit,
        death
    }

    public EnemyState undeadState = EnemyState.dead;

    void Start()
    {
        
    }

    void Update()
    {
        switch (undeadState) 
        {
            case EnemyState.dead:
                break;
            case EnemyState.stand:
                stand();
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
                break;
        }
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

    private void stand()
    {
        // �÷��̾ ���� ���� �ȿ� ������ �Ͼ�� �ִϸ��̼��� ���
        // �ִϸ��̼� ����� ������ �÷��̾ ���� �ٶ󺻴�
        // attack ���� ��ȯ
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.tag == "Sword") 
        {
            hitted();
        }
    }
}
