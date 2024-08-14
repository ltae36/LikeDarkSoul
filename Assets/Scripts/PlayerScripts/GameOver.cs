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


    void Start()
    {
        child = child.gameObject;
        ragDoll.SetActive(false);
        uDie.SetActive(false);
        blackOut.SetActive(false);
    }

    void Update()
    {
        // dead상태가 되면 
        if (hp.mystate == StatManager.PlayerState.dead)
        {
            // UI가 실행된다.
            uDie.SetActive(true);
            // child에 있는 플레이어를 비활성화하고 ragDoll을 실행한다.
            child.SetActive(false);
            ragDoll.SetActive(true);
            // 블랙아웃이 실행된다.
            blackOut.SetActive (true);
        }
    }
}
