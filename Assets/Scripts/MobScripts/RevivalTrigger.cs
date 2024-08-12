using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevivalTrigger : MonoBehaviour
{
    public Animator anim;

    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player") 
        {
            anim.SetTrigger("IsAwake");
        }
    }
}
