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
    public GameObject camera;
   

    PlayerAttack pa;
    CameraControll cc;

    bool chek;
    bool chek2;

    void Start()
    {
        pa = player.GetComponent<PlayerAttack>();
        cc = camera.GetComponent<CameraControll>();
        chek = false;
        chek2 = false;
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
                // 마우스 공격과 카메라 이동이 멈춤
                pa.enabled = false;
                cc.enabled = false;
            }
            // 메뉴가 열린 상태에서 한번 더 esc를 누르면 메뉴가 종료된다.
            else if (chek) 
            {
                chek = false;
                potionInven.SetActive(true);
                stopSceneImage1.SetActive(false);
                pa.enabled = true;
                cc.enabled = true;

                if (chek2)
                {
                    chek2 = false;
                    stopSceneimage2.SetActive(false);
                }
            } 
        }
    }

    // 버튼을 설정 버튼을 눌리면 인벤토리 화면이 열린다.
    public void OpenInventory() 
    {
        chek2 = true;
        stopSceneImage1.SetActive(false);
        stopSceneimage2.SetActive(true);
    }

    // 종료버튼을 누르면 게임이 종료되고 시작화면이 열린다.
    public void QuitGame() 
    {
        SceneManager.LoadScene(0);
    }
}
