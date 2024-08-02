using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{

    [HideInInspector]
    public static BossAttack instance;

    [Header("Attack range")]
    public float nearAttackRange;
    public float normalAttackRange;
    public float farAttackRange;

    BossLocomotion locomotion;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        locomotion = BossLocomotion.instance;
    }



    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, nearAttackRange);
        Gizmos.DrawWireSphere(transform.position, normalAttackRange);
        Gizmos.DrawWireSphere(transform.position, farAttackRange);
    }

    public string[] comboList = { "vertical", "vertical" };

    public float timer;

    private void Update()
    {
        if(timer > 0)
            timer -= Time.deltaTime;
           
    }
}
