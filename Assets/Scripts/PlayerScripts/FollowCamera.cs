using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    // 카메라에 광선을 쏴서 카메라 앞에 플레이어가 아닌 다른 오브젝트가 있다면 플레이어가 보이도록 closeUp한다.

    Camera cam;
    CameraControll camCon;

    public float fov = 20;
    public float rayDist = 500.0f;
    public GameObject player;

    Vector3 myPos;
    public Transform origin;
    public Transform cuPos;

    Ray ray;
    RaycastHit hit;

    void Start()
    {
        cam = GetComponent<Camera>();
        camCon = GetComponentInParent<CameraControll>();
    }

    void Update()
    {
        //Vector3 dir = origin.position - player.transform.position;
        //dir = transform.TransformDirection(dir);

        //myPos = cam.transform.position;

        //if (camCon.mouseX < -25)
        //{
        //    cam.fieldOfView = fov;
        //}
        //else
        //{
        //    cam.fieldOfView = 60;
        //}

        //// 나의 방향에서 플레이어 방향으로 ray를 발사한다.
        //Ray ray = new Ray(player.transform.position, dir.normalized);
        //RaycastHit hitInfo;

        //if (Physics.Raycast(ray, out hitInfo, dir.magnitude, ~(1 << 7)))
        //{
        //    // myPos의 위치를 장애물 앞쪽으로 10cm정도 앞으로 조정한다.
        //    Vector3 hitPosition = hitInfo.point + hitInfo.normal * 0.1f;
        //   transform.position = hitPosition - transform.position;

        //    //// 위에서 계산한 endPos (방향)벡터를 플레이어가 회전된 만큼 회전시킨다.
        //    //// 카메라는 플레이어의 뒤쪽에 있으므로 Quaternion 방향을 역방향으로 계산한다.
        //    //endPos = Quaternion.Inverse(transform.rotation) * endPos;
        //}


        Vector3 distance = cam.transform.position - player.transform.position;

        ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(myPos, distance, rayDist))
        {

            if (hit.collider.tag == "Wall")
            {

            }
            else if (hit.collider.tag == "Player")
            {
                transform.position = origin.position;
            }
        }


    }
}
