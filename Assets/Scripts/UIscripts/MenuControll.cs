using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControll : MonoBehaviour
{
    public GameObject potionInven;
    public GameObject stopSceneImage1;
    public GameObject stopSceneimage2;
    public GameObject player;
   

    PlayerAttack pa;

    bool chek;

    void Start()
    {
        pa = player.GetComponent<PlayerAttack>();
        chek = false;
        stopSceneimage2.SetActive(false);
    }

    void Update()
    {
        // esc를 누르면 포션창이 사라지고 메뉴 UI가 나타난다.
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            if (!chek)
            {
                chek = true;
                potionInven.SetActive(false);
                stopSceneImage1.SetActive(true);
                pa.enabled = false;
            }
            else if (chek) 
            {
                chek = false;
                potionInven.SetActive(true);
                stopSceneImage1.SetActive(false);
                pa.enabled = true;
            }
        }
    }

    public void OpenInventory() 
    {
        stopSceneImage1.SetActive(false);
        stopSceneimage2.SetActive(true);
    }

    public void QuitGame() 
    {
        SceneManager.LoadScene(0);
    }
}
