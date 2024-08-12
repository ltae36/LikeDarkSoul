using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DeathSceneUI : MonoBehaviour
{
    Image image;

    void Start()
    {
        image = GetComponent<Image>();
        StartCoroutine(ActiveDeathScene());
    }

    //IEnumerator ActiveDeathScene() 
    //{
    //    Color color = image.color;
    //    for (int i = 0; i < 100; i++) 
    //    {
    //        color.a += Time.deltaTime * 0.01f;
    //        image.color = color;
    //    }
    //    yield return null;


    //    Color initialColor = image.color;
    //    Color targetColor = new Color(image.color.r, image.color.g, image.color.b, 250); // ���� ��ǥ ���� ���� 1�� �����մϴ�.

    //    float elapsedTime = 0f;

    //    while (elapsedTime < duration)
    //    {
    //        elapsedTime += Time.deltaTime;
    //        image.color = Color.Lerp(initialColor, targetColor, elapsedTime / duration * uDieRate);
    //        yield return null;
    //    }

    //    image.color = targetColor; // ���� ���� ����
    //}

    IEnumerator ActiveDeathScene()
    {
        float currentTime = 0;
        // �ٸ� ��� �Լ��� ���� ������ ��ٸ�
        yield return new WaitForEndOfFrame();

        currentTime += Time.deltaTime;

        // UI�� ������ ������ ��������.
        Color uDieAlpah = image.color;
        uDieAlpah.a += Time.deltaTime;

        image.color = uDieAlpah;
    }
}
