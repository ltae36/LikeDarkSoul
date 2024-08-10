using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [SerializeField] int health;
    public int maxHealth = 1037;

    BossFSM BossFSM;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        BossFSM = GetComponent<BossFSM>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            health = 0;
            if (BossFSM != null && BossFSM.enabled == true)
            {
                BossFSM.Die();
            }
        }
    
    }

    public int GetHp()
    {
        return health;
    }
}
