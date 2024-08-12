using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleStart : MonoBehaviour
{
    public GameObject image1;
    public GameObject image2;

    void Start()
    {
    }

    private void Update()
    {
        if (Input.anyKeyDown) 
        {
            image1.SetActive(false);
            image2.SetActive(true);
        }
    }

}
