using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Tear : MonoBehaviour
{

    PlayerController playerController;
    Animator tearBoomAnim;
    Enemy enemy;

    Vector3 tearPosition;
    Vector3 playerPosition;

    float betweenDistance;

    float playerTearSize;

    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        tearBoomAnim = GetComponent<Animator>();
        enemy = GetComponent<Enemy>();
    }

    void Update()
    {
        playerTearSize = PlayerManager.instance.playerTearSize;
        TearRange();
        TearSize();
    }

    void TearRange()
    {
        //플레이어 위치
        playerPosition = playerController.transform.position;
        //총알 위치
        tearPosition = this.transform.position;
        //둘 사이의 거리
        betweenDistance = Vector3.Distance(tearPosition, playerPosition);

        //둘 사이의 거리가 플레이어 사거리보다 커지면
        if (betweenDistance >= PlayerManager.instance.playerRange)
        {
            //눈물 터지는 애니메이션 실행
            tearBoomAnim.SetTrigger("BoomTear");
        }
    }

    public void StopTear()
    {
        //총알 오브젝트 속도를 zero로 만듬
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    public void TearSize()
    {   
        gameObject.transform.localScale = new Vector3(playerTearSize, playerTearSize, playerTearSize);
    }

    public void DestoryTear()
    {
        //총알 파괴
        Destroy(gameObject);
    }


    private void OnTriggerStay2D(Collider2D collision)
    {

        //벽에 박으면 총알 터트리기
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Object_Rock"))
        {
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            tearBoomAnim.SetTrigger("BoomTear");

        }
        //똥에 박으면
        else if (collision.gameObject.CompareTag("Object_Poop"))
        {
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            tearBoomAnim.SetTrigger("BoomTear");
            collision.GetComponent<Poop>().GetDamage();
        }
        //불에 박으면
        else if (collision.gameObject.CompareTag("Object_Fire"))
        {
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            tearBoomAnim.SetTrigger("BoomTear");
            collision.GetComponent<FirePlace>().GetDamage();
        }
        //적과 박으면
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            tearBoomAnim.SetTrigger("BoomTear");
            collision.gameObject.GetComponent<Enemy>().GetDamage(PlayerManager.instance.playerDamage);

            //Rigidbody2D enemyRB = collision.gameObject.GetComponent<Rigidbody2D>();

            //총알 방향
            Vector2 direction = gameObject.transform.GetComponent<Rigidbody2D>().velocity;
            StartCoroutine(collision.gameObject.GetComponent<Enemy>().knockBack());
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(direction*300);
        }
    }
}