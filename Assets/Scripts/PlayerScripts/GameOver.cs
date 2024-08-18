using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public StatManager hp;
    public GameObject child;
    public GameObject ragDoll;
    public GameObject uDie;
    public GameObject blackOut;

    public BossHealthBarController hpController1;
    public BossHealthBarController hpController2;


    void Start()
    {
        child = child.gameObject;
        ragDoll.SetActive(false);
        uDie.SetActive(false);
        blackOut.SetActive(false);
    }

    void Update()
    {
        // dead���°� �Ǹ� 
        if (hp.mystate == StatManager.PlayerState.dead)
        {
            // UI�� ����ȴ�.
            uDie.SetActive(true);
            // child�� �ִ� �÷��̾ ��Ȱ��ȭ�ϰ� ragDoll�� �����Ѵ�.
            child.SetActive(false);
            ragDoll.SetActive(true);
            // ���ƿ��� ����ȴ�.
            blackOut.SetActive (true);

            hpController1.HideBossHpBar();
            hpController2.HideBossHpBar();
        }
    }
}
