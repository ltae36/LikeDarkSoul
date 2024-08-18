using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHit : MonoBehaviour
{

    public PlayerMove playerMove;

    public float damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") )
        {
            playerMove.PlayerHit(damage);
        }
    }
}
