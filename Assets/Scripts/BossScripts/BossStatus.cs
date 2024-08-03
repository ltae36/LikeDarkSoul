using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStatus : MonoBehaviour
{
    [Header("status")]
    public static float moveSpeed = 5;
    public static float dashSpeed = 10;
    public static float rotationSpeed = 1;
    public static float jumpSpeed;

    public static void SetJumpSpeed(float value)
    {
        jumpSpeed = value;
    }
}
