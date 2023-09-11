using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    
    // camera's position should be same as the car's position

    // SeriallizeField 이용해 reference 가져옴
    [SerializeField] GameObject thingToFollow;

    // logic 제일 마지막에 수행하는 LateUpdate, car의 위치변화가 모두 이뤄지고 나서 카메라가 변화함(더 부드럽게)
    void LateUpdate()
    {   
        // position은 Vector3(객체)
        // 카메라가 차를 일정 높이 이상에서 따라가게끔
        transform.position = thingToFollow.transform.position + new Vector3(0, 0, -10);
    }
}
