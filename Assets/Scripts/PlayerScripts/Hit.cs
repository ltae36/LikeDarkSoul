using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    //public BossHealth health;
    public GameObject effect;
    public AudioSource se;
    public int damage;
    BossHealth health;
    EnemyFSM enemyFSM;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Boss") || other.gameObject.CompareTag("Enemy"))
        {
            print("АјАн");
            Instantiate(effect);
            se.Play();
            effect.transform.position = transform.position;

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

    public void SetDamage1000()
    {
        damage = 1000;
    }


}
