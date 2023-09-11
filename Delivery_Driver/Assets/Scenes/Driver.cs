using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver : MonoBehaviour
{
    // [] : attribute 다음에 나오는것에  []안의 것을 적용
    // instepcter 상의 값이 코드 상의 값을 덮어쓴다(실제 코드에는 변화 X)
    [SerializeField] float steerSpeed = 1f;
    [SerializeField] float moveSpeed = 20f;
    [SerializeField] float slowSpeed = 15f;
    [SerializeField] float boostSpeed = 30f;

    void Update()
    {   
        // string reference ("Horizontal" or "Vertical)
        float steerAmount = Input.GetAxis("Horizontal") * steerSpeed * Time.deltaTime;
        float moveAmount = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        // 1초 = 프레임 수 * 프레임 간 간격(시간 = 주기) Time.deltaTime은 프레임 간 간격(주기) => 곱해주면 해당 동작 1초동안 한번 수행
        // 1초에 100번(100 프레임) vs 1초에 200번(200 프레임) 각 deltaTime 은 0.01(1/100) vs 0.005(1/200) 곱해주면 결과는 같은 1초에 1회
        
        // z(안으로 파고드는)축이 좌우 회전 담당 , 음수 : 우회전(시계방향), 양수 : 좌회전(시계 반대방향)
        transform.Rotate(0, 0, -steerAmount);
        transform.Translate(0, moveAmount, 0);
    }


    void OnCollisionEnter2D(Collision2D other)
    {   
        // Trigger가 아니라 Collsion인 모든 오브젝트는 부딪혔을때 속도가 줄어든다
        //if(other.collider.tag == "Obstacle")
        {
            moveSpeed = slowSpeed;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {   
        if(other.tag == "Boost")
        {
            moveSpeed = boostSpeed;
        }
    }
}
