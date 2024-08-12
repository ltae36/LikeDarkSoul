using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public StatManager hp;
    public GameObject child;
    public GameObject ragDoll;

    void Start()
    {
        child = child.gameObject;
        ragDoll.SetActive(false);
    }

    void Update()
    {

        if (hp.mystate == StatManager.PlayerState.dead)
        {
            child.SetActive(false);
            ragDoll.SetActive(true);
        }
    }
}
