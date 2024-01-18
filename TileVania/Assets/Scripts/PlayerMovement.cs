using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(20f, 20f);
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gunPosition;

    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    float gravityScaleAtStart;
    bool isAlive = true;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRigidbody.gravityScale;

        //이렇게도 가능
        //gunPosition = GetComponentInChildren<Transform>();
    }


    void Update()
    {
        if (!isAlive) { return; }

        Run();
        FlipSprite();
        ClimbLadder();
        Die();
    }

    //Action Move의 Message
    void OnMove(InputValue value)
    {
        if (!isAlive) { return; }

        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (!isAlive) { return; }

        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }

        if (value.isPressed)
        {
            myRigidbody.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    void OnFire(InputValue value)
    {   
        if(!isAlive) { return; }
        Instantiate(bullet, gunPosition.position, transform.rotation);
    }

    void Run()
    {
        //y축 이동은 사다리 타는거 아니면 없게 하고싶다
        //y를 0으로 두면 패 프레임 0이 됬다가 중력이 적용됬다가 하니까 움직임이 이상함
        //myRigidbody.velocity.y는 현재의 속도(y)를 말한다. -> 현재 속도를 유지 -> 중력 정상적으로 받는중ㅌㅌ   
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;

        //FlipSprite에서 수평 움직임(속도) 체크하는 bool 변수 똑같이 이용
        //속도가 있다 == 달리고있다 => isRunning을 true로 (isRunning은 Animator의 parameter)
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
    }

    void FlipSprite()
    {
        //수평 움직임이 있는가?(속도가 있는가)
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        //Mathf.Sign이 인자가 0일 때도 1을 반환하기 때문에 이를 방지하기 위함 (가만히 있을때는 그대로 있어야지)
        if (playerHasHorizontalSpeed)
        {
            // scale의 x 값이 양수면 원본, 음수면 좌우반전(뒤집)
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1);
        }
    }

    //사다리 접촉 시 myRigidbody의 gravityScale을 0으로, 아닐 때는 초기 gravityScale(gravityScaleAtStart)
    void ClimbLadder()
    {
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            myRigidbody.gravityScale = gravityScaleAtStart;
            myAnimator.SetBool("isClimbing", false);
            return;
        }

        Vector2 climbVelocity = new Vector2(myRigidbody.velocity.x, moveInput.y * climbSpeed);
        myRigidbody.velocity = climbVelocity;
        myRigidbody.gravityScale = 0;

        bool isPlayerClimbing = Mathf.Abs(myRigidbody.velocity.y) > 0;
        myAnimator.SetBool("isClimbing", isPlayerClimbing);
    }

    //Player 사망 관리, Update에서 실행, 지속적으로 Enemies or Hazards와 접촉하였는지 체크
    void Die()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards")))
        {
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            myRigidbody.velocity = deathKick;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }

}
