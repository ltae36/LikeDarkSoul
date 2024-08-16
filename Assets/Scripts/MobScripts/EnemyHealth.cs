using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int health;
    public int maxHealth = 59;

    public bool isDamaged = false;

    public GameObject hpBar;

    Slider hp;
    EnemyFSM enemyFSM;

    void Start()
    {
        hpBar.SetActive(false);

        health = maxHealth;
        hp.maxValue = maxHealth;
        hp.minValue = 0;

        enemyFSM = GetComponent<EnemyFSM>();
    }

    void Update()
    {
        // 에너미가 공격을 받으면 UI를 표시한다.
        if (enemyFSM.undeadState == EnemyFSM.EnemyState.Hit)
        {
            hpBar.SetActive(true);
        }

        // UI에 hp수치를 적용한다.
        hp.value = health;
    }

    public void TakeDamage(int damage)
    {
        isDamaged = true;
        health -= damage;

        // hp가 0이 되면 사망 상태가 된다.
        if (health <= 0)
        {
            health = 0;
            if (enemyFSM != null && enemyFSM.enabled == true)
            {
                enemyFSM.Death();
            }
        }
    }

    public float GetHp()
    {
        return health;
    }
}
