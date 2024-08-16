using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    [Header("speed")]
    public float moveSpeed = 2;
    public float dashSpeed = 4;
    public float rotationSpeed = 5;

    [Header("health")]
    public int maxHealth = 59;
    public int health;

    private void Start()
    {
        health = maxHealth;
    }
}
