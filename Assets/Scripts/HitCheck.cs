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
        }
        else if (hit.collider.name == "col_sword") 
        {
            enemyDamaged = true;
        }
        else 
        {
            isDamaged= false;
            enemyDamaged = false;
        }
    }
}
