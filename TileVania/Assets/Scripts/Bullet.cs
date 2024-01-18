using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 20f;

    Rigidbody2D myRigidbody;
    PlayerMovement player;
    float xSpeed;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        //Bullet의 Start는 bullet instance가 생성되는 시점, 새 bullet이 생길 때 마다 그때그때 xSpeed 계산
        //transform은 Component의 멤버 변수 (Monobehaviour도 Component 상속함)
        xSpeed = player.transform.localScale.x * bulletSpeed;
    }

    void Update()
    {
        myRigidbody.velocity = new Vector2(xSpeed, 0f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Enemy tag와 접촉 시 접촉한 gameObject(Enemy) 파괴
        if (other.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }

        //Bullet의 파괴 (gameObject == this(Bullet).gameObject)
        Destroy(gameObject);
    }

    //벽(platform) 등고 충돌 시 Bullet 파괴
    void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }
}
