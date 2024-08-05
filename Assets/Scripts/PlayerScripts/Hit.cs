using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    BossHealth health;

    void Start()
    {
        health = GetComponent<BossHealth>();
    }

    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Boss")
        {
            health.TakeDamage(4);
        }
    }
}
