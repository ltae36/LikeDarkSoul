using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    //public BossHealth health;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Boss")
        {
            print("공격");

            BossHealth health = other.GetComponent<BossHealth>();
            if(health != null)
                health.TakeDamage(400);
        }
    }


    //private void OnCollisionEnter(Collision other)
    //{
    //    print(other.gameObject.name);
    //    if (other.gameObject.tag == "Boss")
    //    {
    //        print("공격");
    //        health.TakeDamage(40);
    //    }
    //}

    //private void OnControllerColliderHit(ControllerColliderHit hit)
    //{

    //    print(hit.collider.tag);
    //    if (hit.gameObject.tag == "Boss")
    //    {
    //        print("공격");
    //        health.TakeDamage(40);
    //    }
    //}
}
