using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBarController : MonoBehaviour
{
    public GameObject bossHpBar;


    public void ShowBossHpBar()
    {
        bossHpBar.SetActive(true);
    }

    public void HideBossHpBar()
    {
        bossHpBar.SetActive(false);
    }
}
