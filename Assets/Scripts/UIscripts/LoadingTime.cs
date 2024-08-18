using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingTime : MonoBehaviour
{
    float currentTime;
    public float StarTime = 5f;

    void Start()
    {
        
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime > StarTime) 
        {
            currentTime = 0;
            SceneManager.LoadScene(2);
        }
    }
}
