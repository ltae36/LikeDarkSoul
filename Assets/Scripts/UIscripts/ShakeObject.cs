using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Animations;

public class ShakeObject : MonoBehaviour
{
    public float positionFrequency;
    public Vector3 positionAmplitude;
    public float positionDuration;

    public PositionConstraint positionConstraint;
    Vector3 originPos;

     bool shaking = false;

    private void Start()
    {
        positionConstraint = GetComponent<PositionConstraint>();
    }
    public void ShakePos()
    {
        if(!shaking)
        {
            StartCoroutine(ShakePosition(positionDuration, positionFrequency, positionAmplitude));
        }
    }


    // 포지션 흔들기
    // 지정된 시간 동안 일정한 범위 안에서 일정한 시간 간격으로 위치를 변경한다.
    // 필요 요소: 전체 시간, 진동 횟수, 진폭
    IEnumerator ShakePosition(float duration, float frequency, Vector3 amplitude)
    {
        positionConstraint.enabled = false;
        float interval = 1.0f / frequency;
        originPos = transform.position;
        //FollowCamera.FollowType currentType = followCamComp.followType;

        shaking = true;
        //followCamComp.followType = FollowCamera.FollowType.ShakingPositionType;

        for(float i = 0; i < duration; i += interval)
        {
            // 랜덤 함수를 이용한 랜덤 방식
            Vector2 randomPos = Random.insideUnitCircle;
            randomPos.x *= amplitude.x;
            randomPos.y *= amplitude.y;

            // originPos를 기준을 랜덤한 위치 값을 계산해서 그 쪽으로 위치를 변경한다.
            transform.position = transform.position + new Vector3(randomPos.x, randomPos.y, 0);

            yield return new WaitForSeconds(interval);

        }

        shaking = false;
        transform.position = originPos;
        positionConstraint.enabled = true;
        //followCamComp.followType = currentType;
    }

}
