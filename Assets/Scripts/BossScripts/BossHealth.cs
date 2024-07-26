using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] int maxHealth;
    [SerializeField] bool isDead = false;


    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BossGetDamaged(int damage)
    {
        if(health > 0)
        {
            health -= damage;
        }
        if (health < 0)
        {
            isDead = true;
        }
    }

    public int GetHp()
    {
        return health;
    }
}
