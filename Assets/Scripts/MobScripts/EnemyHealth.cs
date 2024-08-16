using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int health;
    public int maxHealth = 59;

    //public bool isDamaged = false;

    //public GameObject hpBar;

    //Slider hp;
    EnemyFSM enemyFSM;

    void Start()
    {
        //hpBar.SetActive(false);

        //health = maxHealth;
        //hp.maxValue = maxHealth;
        //hp.minValue = 0;

        enemyFSM = GetComponent<EnemyFSM>();
    }

    //void Update()
    //{
    //    // ���ʹ̰� ������ ������ UI�� ǥ���Ѵ�.
    //    if (enemyFSM.undeadState == EnemyFSM.EnemyState.Hit)
    //    {
    //        hpBar.SetActive(true);
    //    }

    //    // UI�� hp��ġ�� �����Ѵ�.
    //    hp.value = health;
    //}

    public void TakeDamage(int damage)
    {
        //isDamaged = true;
        health -= damage;
        

        // hp�� 0�� �Ǹ� ��� ���°� �ȴ�.
        if (health <= 0)
        {
            health = 0;
            if (enemyFSM != null && enemyFSM.enabled == true)
            {
                enemyFSM.undeadState = EnemyFSM.EnemyState.Die;
            }
        }
        else
        {
            if(enemyFSM != null && enemyFSM.enabled == true)
            {
                enemyFSM.undeadState = EnemyFSM.EnemyState.Hit;
            }
        }
    }

    public float GetHp()
    {
        return health;
    }
}
