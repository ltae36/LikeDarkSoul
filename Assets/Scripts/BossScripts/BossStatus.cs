using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStatus : MonoBehaviour
{
    [Header("status")]
    public static float moveSpeed = 2;
    public static float dashSpeed = 20;
    public static float rotationSpeed = 3;
    public static float jumpSpeed;

    public static void SetJumpSpeed(float value)
    {
        jumpSpeed = value;
    }
}
