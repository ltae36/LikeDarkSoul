using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagerRestart : MonoBehaviour
{
    StatManager stat;
    float currentTime = 3.0f;

    void Start()
    {
        stat = GetComponent<StatManager>();
    }

    void Update()
    {
        // dead���°� �ƴٸ� �ڵ����� ������ϴ� GameOver�ڷ�ƾ�� �����Ѵ�.
        if (stat.mystate == StatManager.PlayerState.dead)
        {
            StartCoroutine(GameOver(12.0f));
        }
    }

    IEnumerator GameOver(float gameOverTime) 
    {
        // ���� �ð��� ���� �ڿ� ������ ������Ѵ�.
        yield return new WaitForSeconds(gameOverTime);
        SceneManager.LoadScene(1);
    }
}
