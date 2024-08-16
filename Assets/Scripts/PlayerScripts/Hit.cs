using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    //public BossHealth health;
    public GameObject effect;
    public int damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Boss" || other.gameObject.tag == "Enemy")
        {
            print("АјАн");
            Instantiate(effect);
            effect.transform.position = other.transform.position;

            BossHealth health = other.GetComponent<BossHealth>();
            if(health != null)
                health.TakeDamage(damage);

            EnemyHealth enemyHp = other.GetComponent<EnemyHealth>();
            if (enemyHp != null) 
            {
                enemyHp.TakeDamage(damage);
            }
        }
    }

    IEnumerator effectDestroy() 
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(effect);
    }

}
