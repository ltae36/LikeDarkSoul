using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHit : MonoBehaviour
{
    public BossFSM bossFSM;
    public PlayerMove playerMove;

    public float damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && bossFSM.bossState == BossFSM.BossState.Attack)
        {
            playerMove.PlayerHit(damage);
        }
    }
}
