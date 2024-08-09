using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventScript : MonoBehaviour
{
    public UIManager manager;
    public float phaseTransitionTime = 1.0f;

    GameObject bossPhase1;
    GameObject bossPhase2;

    private void Start()
    {
        bossPhase1 = GameObject.Find("Boss1");
        bossPhase2 = GameObject.Find("Boss2");
    }
    public void HideBossHpBar()
    {
        manager.HideBossHpBar();
    }

    public void BossPhase2()
    {
        StartCoroutine(phaseTransitionProcess());
    }
    
    IEnumerator phaseTransitionProcess()
    {
        yield return new WaitForSeconds(phaseTransitionTime);

        
        bossPhase2.SetActive(true);
        bossPhase2.transform.position = bossPhase1.transform.position;
        bossPhase2.transform.rotation = bossPhase1.transform.rotation;
        bossPhase1.SetActive(false);
    }
}
