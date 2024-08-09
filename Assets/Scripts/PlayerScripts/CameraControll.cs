using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    public float mouseSenstv = 400f;
    public float maxYRot = 70f;
    public float minYRot = -70f;

    public Transform player;
    public float moveSpeed = 0.5f;
    public float rotationSpeed = 0.5f;

    public float playerDistance = 10f;


    float mouseY;
    public float mouseX;



    void Update()
    {
        HandleMove();
        HandleRotate();

    }

    private void HandleRotate() 
    {
        mouseY += Input.GetAxisRaw("Mouse X") * mouseSenstv * Time.deltaTime;
        mouseX -= Input.GetAxisRaw("Mouse Y") * mouseSenstv * Time.deltaTime;

        // 상하 회전 각도를 제한하여 너무 뒤로 회전하지 않도록 합니다.
        mouseX = Mathf.Clamp(mouseX, minYRot, maxYRot);

        // 카메라의 로컬 회전을 설정
        //transform.localRotation = Quaternion.Euler(mouseX, mouseY, 0f);

        Vector3 rotation = Vector3.zero;
        rotation.y = mouseY;
        rotation.x = mouseX;
        transform.rotation = Quaternion.Euler(rotation);

        print(rotation);

    }

    void HandleMove()
    {
        //따라가는 거
        transform.position = Vector3.Lerp(transform.position, player.transform.position, moveSpeed * Time.deltaTime);

        //보는 것
        //Quaternion lookRotation = Quaternion.LookRotation(player.position - Camera.main.transform.position, Vector3.up);
        //Camera.main.transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
    }
}
