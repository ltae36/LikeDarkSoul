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

    public AudioSource uDiedAudio;

    public float time;


    void Start()
    {
        uDiedAudio.enabled = false;
        uDiedAudio.mute = false;
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
            // ���带 �����Ѵ�.
            uDiedAudio.enabled = true;
            //uDiedAudio.Play();
            // UI�� ����ȴ�.
            uDie.SetActive(true);
            // child�� �ִ� �÷��̾ ��Ȱ��ȭ�ϰ� ragDoll�� �����Ѵ�.
            child.SetActive(false);
            ragDoll.SetActive(true);

        }
        Invoke("BlackOut", time);
    }

    void BlackOut() 
    {
        // ���ƿ��� ����ȴ�.
        blackOut.SetActive(true);
    }
}


