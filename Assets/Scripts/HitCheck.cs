using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCheck : DamageCount
{
    void Start()
    {
        
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.name == "E_col_attack")
        {
            isDamaged = true;
            StartCoroutine(DamageTime());
        }
        else if (hit.collider.name == "col_sword") 
        {
            enemyDamaged = true;
            StartCoroutine(DamageTime());
        }
        else if (hit == null)
        {
            isDamaged = false;
            enemyDamaged = false;
        }
    }

    IEnumerator DamageTime() 
    {
        yield return null;
        isDamaged = false ;
        enemyDamaged= false ;
    }
}
