using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    //움직임에 관여하는 스크립트

    //1. 플레이어를 바라보도록 rotation을 바꾼다.
    //2. 목적지를 설정하고, 목적지로 이동한다.
    //3. 목적지로 달려나간다.

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
