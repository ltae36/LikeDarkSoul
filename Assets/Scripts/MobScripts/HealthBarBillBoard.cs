using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarBillBoard : MonoBehaviour
{

    void LateUpdate()
    {
        transform.LookAt(transform.position + Camera.main.transform.forward);
    }
}
