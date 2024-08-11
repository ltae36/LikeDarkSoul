using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCount : MonoBehaviour
{
    public bool isDamaged;
    public bool enemyDamaged;
    public float hitTime;

    void Start()
    {
        hitTime = 0;
        isDamaged = false;
        enemyDamaged = false;
    }
}
