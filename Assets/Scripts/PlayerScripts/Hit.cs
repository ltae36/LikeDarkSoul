using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    //public BossHealth health;
    public GameObject effect;
    public int damage;
    BossHealth health;
    EnemyFSM enemyFSM;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Boss") || other.gameObject.CompareTag("Enemy"))
        {
            print("АјАн");
            Instantiate(effect);
            effect.transform.position = other.transform.position;

            if(other.gameObject.CompareTag("Boss"))
            {
                health = other.gameObject.GetComponent<BossHealth>();
                health.TakeDamage(damage);
            }
            else if (other.gameObject.CompareTag("Enemy"))
            {
                enemyFSM = other.gameObject.GetComponent<EnemyFSM>();
                enemyFSM.TakeDamage(damage);
            }
        }
    }


}
