using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGame : MonoBehaviour
{
    public BossFSM boss;
    public GameObject winUI;

    private void Update()
    {
        if (boss != null)
        {
            if (boss.bossState == BossFSM.BossState.Die)
            {
                winUI.SetActive(true);
            }
        }
    }

}
