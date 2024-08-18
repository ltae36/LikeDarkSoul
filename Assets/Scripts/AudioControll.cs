using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControll : MonoBehaviour
{
    public List<AudioClip> clips = new List<AudioClip>();

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {        
        // 오디오의 재생이 끝났다면
        if (audioSource.time == audioSource.clip.length) 
        {
            // 리스트의 첫번째 클립을 마지막에 추가하고
            clips.Add(clips[0]);
            // 첫번째 클립은 삭제한다.
            clips.RemoveAt(0);

            audioSource.clip = clips[0];

        }
    }
}
