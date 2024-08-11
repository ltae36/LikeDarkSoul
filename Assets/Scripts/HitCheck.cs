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
            DamageTime(1.5f);
        }
        else if (hit.collider.name == "col_sword") 
        {
            enemyDamaged = true;
            DamageTime(1.5f);
        }
        else 
        {
            isDamaged = false;
            enemyDamaged = false;
        }
    }

    IEnumerator DamageTime(float sec) 
    {
        yield return new WaitForSeconds(sec);
        isDamaged = false ;
        enemyDamaged= false ;
    }
}
