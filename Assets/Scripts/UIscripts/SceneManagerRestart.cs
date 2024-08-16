using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagerRestart : MonoBehaviour
{
    StatManager stat;
    public float sceneReloadingTime = 6.0f;

    void Start()
    {
        stat = GetComponent<StatManager>();
    }

    void Update()
    {
        // dead상태가 됐다면 자동으로 재시작하는 GameOver코루틴을 실행한다.
        if (stat.mystate == StatManager.PlayerState.dead)
        {
            StartCoroutine(GameOver(sceneReloadingTime));
        }
    }

    IEnumerator GameOver(float gameOverTime) 
    {
        // 일정 시간이 지난 뒤에 게임을 재시작한다.
        yield return new WaitForSeconds(gameOverTime);
        SceneManager.LoadScene(1);
    }
}
