using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPhaseTransition : MonoBehaviour
{
    GameObject boss1;
    GameObject boss2;
    public float transitionTime = 0.1f;

    private void Start()
    {
        boss1 = transform.Find("Boss1").gameObject;
        boss2 = transform.Find("Boss2").gameObject;
    }
    public void PhaseTransition()
    {
        StartCoroutine(TransitionProcess());
    }
    IEnumerator TransitionProcess()
    {
        yield return new WaitForSeconds(transitionTime);

        Vector3 tempPos = boss1.transform.position;
        Quaternion tempRot = boss1.transform.rotation;
        Destroy(boss1);

        yield return null;
        boss2.SetActive(true);
        boss2.transform.position = tempPos;
        boss2.transform.rotation = tempRot;

        boss2.GetComponent<Animator>().rootPosition = tempPos;
        boss2.GetComponent<Animator>().rootRotation = tempRot;
    }
}
