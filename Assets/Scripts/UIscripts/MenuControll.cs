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
        // esc�� ������ ����â�� ������� �޴� UI�� ��Ÿ����.
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {            
            if (!chek)
            {
                chek = true;
                potionInven.SetActive(false);
                stopSceneImage1.SetActive(true);
                // ���콺 ���ݰ� ī�޶� �̵��� ����
                pa.enabled = false;
                cc.enabled = false;
            }
            // �޴��� ���� ���¿��� �ѹ� �� esc�� ������ �޴��� ����ȴ�.
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

    // ��ư�� ���� ��ư�� ������ �κ��丮 ȭ���� ������.
    public void OpenInventory() 
    {
        chek2 = true;
        stopSceneImage1.SetActive(false);
        stopSceneimage2.SetActive(true);
    }

    // �����ư�� ������ ������ ����ǰ� ����ȭ���� ������.
    public void QuitGame() 
    {
        SceneManager.LoadScene(0);
    }
}
