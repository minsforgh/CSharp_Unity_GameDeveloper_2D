using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float leftPadding;
    [SerializeField] float rightPadding;
    [SerializeField] float topPadding;
    [SerializeField] float bottomPadding;

    Vector2 rawInput;

    Vector2 minBounds;
    Vector2 maxBounds;

    Shooter shooter;

    void Start()
    {   
        shooter = GetComponent<Shooter>();
        InitBounds();
    }

    void Update()
    {
        Move();
    }

    //ViewportToWorldPoint 를 통해 ViewPort의 경게를 world Space로
    void InitBounds()
    {
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
    }

    //newPos = oldPos + dleta
    //Mathf.Clamp + Padding 통해 정해진 범위 (min ~ max bound) 내의 값으로 제한
    void Move()
    {
        Vector2 delta = rawInput * moveSpeed * Time.deltaTime;
        Vector2 newPos = new Vector2();
        newPos.x = Mathf.Clamp(transform.position.x + delta.x, minBounds.x + leftPadding, maxBounds.x - rightPadding);
        newPos.y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y + bottomPadding, maxBounds.y - topPadding    );
        transform.position = newPos;   
    }
    
    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();

    }

    //isPressed는 Input Action의 value (trigger, value ..) type 과 상관없는 InputValue 클래스의 필드이다. (단순 매핑한 Input이 있었는지 여부)
    //눌렀을 - 누르는중 : isFiring == true / 뗐을 때 : isFiring == false
    //Action Type value는 입력과 입력되지 않을 때, 두 번 호출됨 (버튼 누를때 - 뗄 때)
    void OnFire(InputValue value)
    {   
        if(shooter != null)
        {
            shooter.isFiring = value.isPressed;
        }
    }
}
