using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{

    //���� ������ ���� �Ÿ� ������ eŰ�� ������ �̰��� ���̺� ����Ʈ�� �ϰ� �ʹ�.
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
