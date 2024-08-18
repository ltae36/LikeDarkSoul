using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class BossPhaseTransition : MonoBehaviour
{
    GameObject boss1;
    GameObject boss2;

    BossEffect bossEffect;

    public float transitionTime = 0.1f;

    private void Start()
    {
        boss1 = transform.Find("Boss1").gameObject;
        boss2 = transform.Find("Boss2").gameObject;
        bossEffect = GetComponent<BossEffect>();
    }
    public void PhaseTransition()
    {
        StartCoroutine(TransitionProcess());
    }
    IEnumerator TransitionProcess()
    {
        yield return new WaitForSeconds(transitionTime);

        boss1.GetComponent<CharacterController>().enabled = false;
        boss2.SetActive(true);
        
        boss2.transform.position = boss1.transform.position;
       // boss2.GetComponent<Animator>().rootPosition = boss1.GetComponent<Animator>().rootPosition;

        //CopyAnimCharacterTransformToRagdoll(boss1.transform, boss2.transform);
        boss1.SetActive(false);


        bossEffect.ChangeBossToPhase2();
    }

    void CopyAnimCharacterTransformToRagdoll(Transform origin, Transform rag)
    {
        rag.transform.SetLocalPositionAndRotation(origin.transform.localPosition, origin.transform.localRotation);

        //print("origin vs rag" + origin.transform.childCount+" " + rag.transform.childCount+" "+(origin.transform.childCount == rag.transform.childCount));
        for (int i = 0; i < origin.transform.childCount; i++)
        {
            CopyAnimCharacterTransformToRagdoll(origin.transform.GetChild(i), rag.transform.GetChild(i));
        }
    }
}
