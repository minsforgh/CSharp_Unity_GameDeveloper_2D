using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [SerializeField] float reloadDelay = 1f;
    [SerializeField] ParticleSystem finishEffect;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            finishEffect.Play();
            GetComponent<AudioSource>().Play();
            Invoke("ReloadScene", reloadDelay);
        }
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}

//스프라이트 유닛퍼 픽셀으로 크기조정
//interpolate, camera 에서 배경색 변경 scale x 부호 바꾸면 좌우반전 
//follow camera lense ortho size 카메라 거리 조정       
// ctrl + . => 메소드 추출 등의 단축키
// GetComponent -> 같은 타입 컴포넌트 존재 시? => Serialize해서 원하는 것을 넣는 식으로