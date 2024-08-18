using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEffect : MonoBehaviour
{
    public AudioSource audioStep;
    public AudioSource voice;


    public BossFSM phase1FSM;
    public BossFSM phase2FSM;

    BossFSM currentBoss;

    void Start()
    {

        audioStep.enabled = false;
        voice.enabled = false;


        currentBoss = phase1FSM;
    }

    void Update()
    {
        if (currentBoss.bossState == BossFSM.BossState.Awake || currentBoss.bossState == BossFSM.BossState.AttackDelay)
        {
            audioStep.enabled = true;
        }
        else
        {
            audioStep.enabled = false;
        }
        

        if (currentBoss.bossState == BossFSM.BossState.Attack) 
        {
            voice.enabled = true;
        }
    }

    public void ChangeBossToPhase2()
    {
        currentBoss = phase2FSM;
    }
}
