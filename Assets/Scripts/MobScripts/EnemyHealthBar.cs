using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public EnemyStatus status;
    public Slider healthBar;

    private void Update()
    {
        healthBar.value = (float)status.health/status.maxHealth;
    }
}
