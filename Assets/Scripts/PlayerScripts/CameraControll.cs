using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    public float mouseSenstv = 400f;

    public Transform player;
    public float moveSpeed = 0.5f;
    public float rotationSpeed = 0.5f;

    float mouseY;
    float mouseX;

    void Start()
    {
        
    }

    void Update()
    {
        Rotate();
    }

    private void Rotate() 
    {
        mouseY += Input.GetAxisRaw("Mouse X") * mouseSenstv * Time.deltaTime;
        mouseX -= Input.GetAxisRaw("Mouse Y") * mouseSenstv * Time.deltaTime;

        // 상하 회전 각도를 제한하여 너무 뒤로 회전하지 않도록 합니다.
        mouseY = Mathf.Clamp(mouseY, -90f, 90f);

        // 카메라의 로컬 회전을 설정
        transform.localRotation = Quaternion.Euler(mouseX, mouseY, 0f);

        //따라가는 거
        transform.position = Vector3.Lerp(Camera.main.transform.position, player.transform.position, moveSpeed * Time.deltaTime);

        //보는 것
        Quaternion lookRotation = Quaternion.LookRotation(player.position - Camera.main.transform.position, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);

    }
}
