using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] float shakeDuration = 1f;
    [SerializeField] float shakeMagnitude = 0.5f;

    Vector3 initialPosition;
    
    void Start()
    {
        initialPosition = transform.position;
    }

    public void Play()
    {
        StartCoroutine(Shake());
    }

    //카메라 흔들림
    IEnumerator Shake()
    {   
        float elapsedTime = 0;
        while(elapsedTime < shakeDuration)
        {   
            //insideUnitCircle : 단위 원 범위 (-1 ~ 1)
            transform.position = initialPosition + (Vector3)Random.insideUnitCircle * shakeMagnitude;
            //Time.deltaTime은 1/초당frame, WatiForNextFrameUnit을 사용하니, 1초가 지나면 elapsedTime도 1만큼 증가 (Time.deltaTime * frame 수)
            elapsedTime += Time.deltaTime;
            yield return new WaitForNextFrameUnit();
        }
        transform.position = initialPosition;
    }

}
