using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    //�����ӿ� �����ϴ� ��ũ��Ʈ

    //1. �÷��̾ �ٶ󺸵��� rotation�� �ٲ۴�.
    //2. �������� �����ϰ�, �������� �̵��Ѵ�.
    //3. �������� �޷�������.

    NavMeshAgent agent;
    GameObject target;
    Vector3 targetPosition;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
    }

    void SetDestination()
    {
        targetPosition = target.transform.position;
        agent.SetDestination(targetPosition);
    }


}
