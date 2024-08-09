using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCount : MonoBehaviour
{
    public bool isDamaged;
    public bool enemyDamaged;

    void Start()
    {
        isDamaged = false;
        enemyDamaged = false;
    }
}
