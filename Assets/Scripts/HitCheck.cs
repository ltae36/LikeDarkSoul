using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCheck : DamageCount
{
    CharacterController cc;

    void Start()
    {
        cc = GetComponent<CharacterController>();  
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.name == "E_col_attack")
        {
            isDamaged = true;
            cc.enabled = false;
        }
        else if (hit.collider.name == "col_sword") 
        {
            enemyDamaged = true;
            cc.enabled = false;
        }
        else if (hit == null)
        {
            isDamaged = false;
            enemyDamaged = false;
        }
        StartCoroutine(DamageTime());
    }

    IEnumerator DamageTime() 
    {
        yield return null;
        isDamaged = false;
        enemyDamaged= false ;
        cc.enabled = true;

    }
}
