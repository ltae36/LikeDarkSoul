using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePointManager : MonoBehaviour
{

    //현재 유저의 세이브 포인트
    GameObject savePoint;

    private void Start()
    {
        savePoint = GameObject.Find("StartPoinit");
    }

    public void SetSavePoint(GameObject savePoint)
    {
        this.savePoint = savePoint;
    }

    public GameObject GetSavePoint()
    {
        return savePoint;
    }
}
