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
        StartCoroutine(ActiveDeathScene(0.5f, 2f));
    }

    IEnumerator ActiveDeathScene(float uDieRate, float duration) 
    {

        Color initialColor = image.color;
        Color targetColor = new Color(image.color.r, image.color.g, image.color.b, 250); // 최종 목표 알파 값을 1로 설정합니다.

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            image.color = Color.Lerp(initialColor, targetColor, elapsedTime / duration * uDieRate);
            yield return null;
        }

        image.color = targetColor; // 최종 색상 보정
    }

    //void ActiveDeathScene(float uDieRate) 
    //{        
    //    Color uDieAlpah = image.color;
    //    uDieAlpah.a = uDieRate;
    //    Color uDieAlpah2 = new Color(image.color.r, image.color.g, image.color.b, 200);

    //    image.color = Color.Lerp(image.color, uDieAlpah2, uDieRate);

    //}
}
