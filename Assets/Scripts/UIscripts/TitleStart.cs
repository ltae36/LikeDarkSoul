using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleStart : MonoBehaviour
{
    public GameObject image1;
    public GameObject image2;

    void Start()
    {
        NextScene();
    }

    private void Update()
    {
        if (Input.anyKeyDown) 
        {
            image1.SetActive(false);
            image2.SetActive(true);
        }
    }

    public void NextScene() 
    {
        SceneManager.LoadScene(1);
    }
}
