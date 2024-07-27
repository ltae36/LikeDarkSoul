using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Animator attack;
    PlayerMove move;

    void Start()
    {
        attack = GetComponent<Animator>();
        move = GetComponent<PlayerMove>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            attack.SetTrigger("Sword");
        }
        if (Input.GetMouseButton(1))
        {
            attack.SetBool("Shield", move.dir == Vector3.zero);
        }
        else if (Input.GetMouseButtonUp(1))
        {
            attack.SetBool("Shield", move.dir == Vector3.zero);
        }
        
    }
}
