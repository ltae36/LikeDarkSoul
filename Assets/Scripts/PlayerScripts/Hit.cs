using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    public BossHealth health;

    void Start()
    {

    }

    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        print(other.name);
        if (other.gameObject.tag == "Boss")
        {
            print("����");
            health.TakeDamage(400);
        }
    }


    //private void OnCollisionEnter(Collision other)
    //{
    //    print(other.gameObject.name);
    //    if (other.gameObject.tag == "Boss")
    //    {
    //        print("����");
    //        health.TakeDamage(40);
    //    }
    //}

    //private void OnControllerColliderHit(ControllerColliderHit hit)
    //{

    //    print(hit.collider.tag);
    //    if (hit.gameObject.tag == "Boss")
    //    {
    //        print("����");
    //        health.TakeDamage(40);
    //    }
    //}
}