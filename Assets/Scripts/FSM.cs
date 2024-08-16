using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM : MonoBehaviour
{
    public enum EnemyType
    {
        Enemy,
        Boss
    }

    public EnemyType enemyType;
}
