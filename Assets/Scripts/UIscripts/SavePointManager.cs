using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePointManager : MonoBehaviour
{

    //���� ������ ���̺� ����Ʈ
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
