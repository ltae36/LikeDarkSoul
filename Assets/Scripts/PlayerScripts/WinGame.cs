using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGame : MonoBehaviour
{
    public BossFSM boss;
    public GameObject winUI;
    public float waitingTime = 2.0f;

    private void Update()
    {
        if (boss != null)
        {
            if (boss.bossState == BossFSM.BossState.Die)
            {
                Invoke("StartUI", waitingTime);
                this.enabled = false;
            }
        }
    }

    void StartUI()
    {
        winUI.SetActive(true);
    }
}
