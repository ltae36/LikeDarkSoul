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

    public StatManager statManager;
    GameObject followTarget;

    float mouseY = 90;
    public float mouseX = 30;

    public enum CameraState
    {
        MouseInput,
        LockOn
    }

    CameraState camState = CameraState.MouseInput;

    void SetMouseXY()
    {
        mouseX = transform.eulerAngles.x;
        mouseY = transform.eulerAngles.y;
    }

    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (camState == CameraState.LockOn) {
                camState = CameraState.MouseInput; 
                SetMouseXY();
            }
            else 
            {
                //플레이어 주위에 적이 있을 때
                Collider[] colliders = Physics.OverlapSphere(transform.position, 20.0f,1<<10);
                if (colliders.Length > 0)
                {
                    Collider nearEnemy = colliders[0];
                    foreach (Collider collider in colliders)
                    {
                        if(Vector3.Distance(nearEnemy.transform.position,transform.position) > Vector3.Distance(collider.transform.position,transform.position))
                        {
                            nearEnemy = collider;
                        }    
                    }
                    followTarget = nearEnemy.gameObject;
                    print(followTarget);
                    camState = CameraState.LockOn; 
                }

            }
        }

        if (statManager.mystate == StatManager.PlayerState.dead)
        {
            this.enabled = false;
        }
        //Invoke("HandleRotate", 2.5f);
        switch (camState)
        {
            case CameraState.MouseInput:
                HandleRotate();
                break;
            case CameraState.LockOn:
                LockOn();
                break;

        }
        HandleMove();
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

        //print(rotation);

    }

    private void LockOn()
    {
        transform.forward = followTarget.transform.position - transform.position;
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
