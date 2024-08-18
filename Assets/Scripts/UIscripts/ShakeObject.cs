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


    // ������ ����
    // ������ �ð� ���� ������ ���� �ȿ��� ������ �ð� �������� ��ġ�� �����Ѵ�.
    // �ʿ� ���: ��ü �ð�, ���� Ƚ��, ����
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
            // ���� �Լ��� �̿��� ���� ���
            Vector2 randomPos = Random.insideUnitCircle;
            randomPos.x *= amplitude.x;
            randomPos.y *= amplitude.y;

            // originPos�� ������ ������ ��ġ ���� ����ؼ� �� ������ ��ġ�� �����Ѵ�.
            transform.position = transform.position + new Vector3(randomPos.x, randomPos.y, 0);

            yield return new WaitForSeconds(interval);

        }

        shaking = false;
        transform.position = originPos;
        positionConstraint.enabled = true;
        //followCamComp.followType = currentType;
    }

}
