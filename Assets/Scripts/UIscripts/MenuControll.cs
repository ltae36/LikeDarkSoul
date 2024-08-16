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

    //메뉴 창이 열려있는지 check하는 변수
    bool isMenuOpen;
    
    bool isStopSceneOpen;

    void Start()
    {
        pa = player.GetComponent<PlayerAttack>();
        cc = camera.GetComponent<CameraControll>();
        isMenuOpen = false;
        isStopSceneOpen = false;
        stopSceneimage2.SetActive(false);
    }

    void Update()
    {
        // esc를 누르면 포션창이 사라지고 메뉴 UI가 나타난다.
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {            
            if (!isMenuOpen)
            {
                isMenuOpen = true;

                //밑의 장비창을 끈다.
                potionInven.SetActive(false);

                //메뉴 창이 켜진다.
                stopSceneImage1.SetActive(true);
                // 마우스 공격과 카메라 이동이 멈춤
                pa.enabled = false;
                cc.enabled = false;
            }
            // 메뉴가 열린 상태에서 한번 더 esc를 누르면 메뉴가 종료된다.
            else if (isMenuOpen) 
            {
                isMenuOpen = false;

                //장비창이 켜진다.
                potionInven.SetActive(true);
                //메뉴 창을 끈다.
                stopSceneImage1.SetActive(false);

                //마우스 공격과 카메라 이동을 다시 시작한다.
                pa.enabled = true;
                cc.enabled = true;

                if (isStopSceneOpen)
                {
                    isStopSceneOpen = false;
                    stopSceneimage2.SetActive(false);
                }
            } 
        }
    }

    // 버튼을 설정 버튼을 눌리면 인벤토리 화면이 열린다.
    public void OpenInventory() 
    {
        isStopSceneOpen = true;
        stopSceneImage1.SetActive(false);
        stopSceneimage2.SetActive(true);
    }

    // 종료버튼을 누르면 게임이 종료되고 시작화면이 열린다.
    public void QuitGame() 
    {
        SceneManager.LoadScene(0);
    }
}
