using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHpBar : MonoBehaviour
{
    public Slider bossHelathBar;

    public BossHealth bossHealth;

    private void Update()
    {
        bossHelathBar.value = bossHealth.GetHp() / bossHealth.maxHealth;
    }


}
