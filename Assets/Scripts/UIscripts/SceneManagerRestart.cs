using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if (stat.mystate == StatManager.PlayerState.dead)
        {
            StartCoroutine(GameOver(10.0f));
            Time.timeScale = 1.0f;
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene(1);
            }
        }
    }

    IEnumerator GameOver(float gameOverTime) 
    {
        Time.timeScale = 0;

        yield return new WaitForSeconds(gameOverTime);

    }
}
