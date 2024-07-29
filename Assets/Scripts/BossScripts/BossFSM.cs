using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFSM : MonoBehaviour
{

    public enum BossState
    {
        Sleep,
        Awake,
        Idle,
        Attack,
        Die
    }

    BossState myState;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (myState)
        {

        }
    }
}
