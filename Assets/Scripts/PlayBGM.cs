using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBGM : MonoBehaviour
{
    public AudioSource[] bgm = new AudioSource[2];

    void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player") 
        {
            bgm[0].Stop();
            bgm[1].Play();
        }
    }
}
