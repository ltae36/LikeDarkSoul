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
    //    Color targetColor = new Color(image.color.r, image.color.g, image.color.b, 250); // 최종 목표 알파 값을 1로 설정합니다.

    //    float elapsedTime = 0f;

    //    while (elapsedTime < duration)
    //    {
    //        elapsedTime += Time.deltaTime;
    //        image.color = Color.Lerp(initialColor, targetColor, elapsedTime / duration * uDieRate);
    //        yield return null;
    //    }

    //    image.color = targetColor; // 최종 색상 보정
    //}

    IEnumerator ActiveDeathScene()
    {
        float currentTime = 0;
        // 다른 모든 함수가 끝날 때까지 기다림
        yield return new WaitForEndOfFrame();

        currentTime += Time.deltaTime;

        // UI의 투명도가 서서히 진해진다.
        Color uDieAlpah = image.color;
        uDieAlpah.a += Time.deltaTime;

        image.color = uDieAlpah;
    }
}
