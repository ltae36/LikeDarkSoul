using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStatus : MonoBehaviour
{
    [Header("status")]
    public float moveSpeed = 2;
    public float dashSpeed = 20;
    public float rotationSpeed = 3;


    [Header("Distance")]
    public float awakeDistance = 20;
    public float attackDistance = 6;

    [Header("Time")]
    public float idleTime = 3;
    public float jumpTime = 3.0f;
    public float jump_x_velocity;
    public float jump_y_velocity;



}
