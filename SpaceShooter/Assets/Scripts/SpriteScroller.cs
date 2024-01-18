using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteScroller : MonoBehaviour
{
    [SerializeField] Vector2 moveSpeed;

    Vector2 offset;
    Material material;
    
    void Awake()
    {   
        material = GetComponent<SpriteRenderer>().material;
    }

    void Update()
    {   
        // offset은 1초에 moveSpeed 만큼, 배경이 1초마다 moveSpeed만큼 움직임
        offset = moveSpeed * Time.deltaTime;
        material.mainTextureOffset += offset;
    }
}
