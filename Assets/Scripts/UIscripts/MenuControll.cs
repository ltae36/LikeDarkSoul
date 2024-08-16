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

    //�޴� â�� �����ִ��� check�ϴ� ����
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
        // esc�� ������ ����â�� ������� �޴� UI�� ��Ÿ����.
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {            
            if (!isMenuOpen)
            {
                isMenuOpen = true;

                //���� ���â�� ����.
                potionInven.SetActive(false);

                //�޴� â�� ������.
                stopSceneImage1.SetActive(true);
                // ���콺 ���ݰ� ī�޶� �̵��� ����
                pa.enabled = false;
                cc.enabled = false;
            }
            // �޴��� ���� ���¿��� �ѹ� �� esc�� ������ �޴��� ����ȴ�.
            else if (isMenuOpen) 
            {
                isMenuOpen = false;

                //���â�� ������.
                potionInven.SetActive(true);
                //�޴� â�� ����.
                stopSceneImage1.SetActive(false);

                //���콺 ���ݰ� ī�޶� �̵��� �ٽ� �����Ѵ�.
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

    // ��ư�� ���� ��ư�� ������ �κ��丮 ȭ���� ������.
    public void OpenInventory() 
    {
        isStopSceneOpen = true;
        stopSceneImage1.SetActive(false);
        stopSceneimage2.SetActive(true);
    }

    // �����ư�� ������ ������ ����ǰ� ����ȭ���� ������.
    public void QuitGame() 
    {
        SceneManager.LoadScene(0);
    }
}
