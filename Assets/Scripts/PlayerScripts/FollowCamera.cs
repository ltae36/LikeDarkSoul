using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    // ī�޶� ������ ���� ī�޶� �տ� �÷��̾ �ƴ� �ٸ� ������Ʈ�� �ִٸ� �÷��̾ ���̵��� closeUp�Ѵ�.

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

        //// ���� ���⿡�� �÷��̾� �������� ray�� �߻��Ѵ�.
        //Ray ray = new Ray(player.transform.position, dir.normalized);
        //RaycastHit hitInfo;

        //if (Physics.Raycast(ray, out hitInfo, dir.magnitude, ~(1 << 7)))
        //{
        //    // myPos�� ��ġ�� ��ֹ� �������� 10cm���� ������ �����Ѵ�.
        //    Vector3 hitPosition = hitInfo.point + hitInfo.normal * 0.1f;
        //   transform.position = hitPosition - transform.position;

        //    //// ������ ����� endPos (����)���͸� �÷��̾ ȸ���� ��ŭ ȸ����Ų��.
        //    //// ī�޶�� �÷��̾��� ���ʿ� �����Ƿ� Quaternion ������ ���������� ����Ѵ�.
        //    //endPos = Quaternion.Inverse(transform.rotation) * endPos;
        //}


        //Vector3 distance = cam.transform.position - player.transform.position;

        //ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        //if (Physics.Raycast(myPos, distance, rayDist))
        //{

        //    if (hit.collider.tag == "Wall")
        //    {

        //    }
        //    else if (hit.collider.tag == "Player")
        //    {
        //        transform.position = origin.position;
        //    }
        //}


    }
}
