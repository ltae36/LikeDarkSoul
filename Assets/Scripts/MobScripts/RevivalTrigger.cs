using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevivalTrigger : MonoBehaviour
{
    public Animator anim;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player") 
        {
            anim.SetBool("inPlayer", true);
        }
    }
}
