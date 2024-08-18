using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEffect : MonoBehaviour
{
    public AudioSource audioStep;
    public AudioSource voice;
    public AudioSource clear;

    public GameObject clearScene;
    public GameObject effect;

    BossFSM fSM;

    void Start()
    {
        fSM = GetComponentInChildren<BossFSM>();
        clearScene.SetActive(false);
        effect.SetActive(false);
        audioStep.enabled = false;
        voice.enabled = false;
        clear.enabled = false;
    }

    void Update()
    {
        if (fSM.bossState == BossFSM.BossState.Awake || fSM.bossState == BossFSM.BossState.AttackDelay)
        {
            audioStep.enabled = true;
        }
        else if (fSM.bossState == BossFSM.BossState.Die) 
        {
 
            Invoke("BossClear", 3f);
        }
        else 
        {
            audioStep.enabled = false;
        }
        if (fSM.bossState == BossFSM.BossState.Attack) 
        {
            voice.enabled = true;
        }
    }

    void BossClear() 
    {
        clear.enabled = true;
        clearScene.SetActive(true);
        effect.SetActive(true);
    }
}
