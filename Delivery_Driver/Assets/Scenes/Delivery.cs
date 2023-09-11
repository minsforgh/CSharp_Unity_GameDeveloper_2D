using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//새 함수 작성 시 새 스크립트를 짜는 것이 건강한 습관
public class Delivery : MonoBehaviour
{   

    [SerializeField] Color32 hasPackageColor = new Color32(1, 1, 1, 1);
    [SerializeField] Color32 noPackageColor = new Color32(1, 1, 1, 1);

    [SerializeField] float destroyDelay = 0.5f;

    bool hasPackage;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    //Collision은 충돌하는 오브젝트 뿐만 아니라, 충돌 자체에 대한 정보를 가져옴 (Collsion2D가 parameter type인 이유)
    void OnCollisionEnter2D(Collision2D other)
    {   
        //충돌 여부를 콘솔에서 확인 가능 
        Debug.Log("Ouch!!");
    }

    //반면 Trigger는 접촉하는 오브젝트 자체에만 집중함(Collider2D가 parameter type)
    //Collison.collider와 같이 Collsion에서 충돌한 물체인 Collider를 가져올수있지만, Collider Class가 Collsion의 하위 클래스라는건 아니다.
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Package" && !hasPackage)
        {
            Debug.Log("Package picked up");

            // GameObject를 scene에서 삭제 삭제할 GameObejct, 삭제까지 걸리는 시간(delay)를 요구
            Destroy(other.gameObject, destroyDelay);
            hasPackage = true;
            spriteRenderer.color = hasPackageColor;
        }

        if (other.tag == "Customer" && hasPackage)
        {
            Debug.Log("Package delivered");
            hasPackage = false;
            spriteRenderer.color = noPackageColor;
        }
    }
    
}
