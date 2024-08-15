using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStatus : MonoBehaviour
{
    [Header("status")]
    public float moveSpeed = 2;
    public float dashSpeed = 20;
    public float rotationSpeed = 3;
    public float jumpSpeed;

    public  void SetJumpSpeed(float time)
    {
        jumpSpeed = Physics.gravity.y * time / 2 * -1;
    }
}
