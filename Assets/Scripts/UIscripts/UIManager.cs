using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject bossHpBar;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowBossHpBar()
    {
        bossHpBar.SetActive(true);
    }

    public void HideBossHpBar()
    {
        bossHpBar.SetActive(false);
    }
}
