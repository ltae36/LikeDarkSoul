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
        // ������� ����� �����ٸ�
        if (audioSource.time == audioSource.clip.length) 
        {
            // ����Ʈ�� ù��° Ŭ���� �������� �߰��ϰ�
            clips.Add(clips[0]);
            // ù��° Ŭ���� �����Ѵ�.
            clips.RemoveAt(0);

            audioSource.clip = clips[0];

        }
    }
}
