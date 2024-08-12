using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{

    //만약 유저가 일정 거리 내에서 e키를 누르면 이곳을 세이브 포인트로 하고 싶다.
    public float saveRadius = 5f;
    // Update is called once per frame
    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, saveRadius);
        foreach(Collider collider in colliders)
        {
            if (collider.tag.Equals("Player"))
            {
                InputManager.instance.SetSavePointActive();
            }
        }
    }
}
