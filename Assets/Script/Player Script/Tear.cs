using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Tear : MonoBehaviour
{

    PlayerController playerController;
    Animator tearBoomAnim;

    Vector3 tearPosition;
    Vector3 playerPosition;

    float betweenDistance;

    float playerTearSize;

    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        tearBoomAnim = GetComponent<Animator>();
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

    public void KnockBack()
    {
        Vector2 a = gameObject.transform.GetComponent<Rigidbody2D>().velocity;
        a.y = a.y* -1f;
        a.x = a.x * -1f;
        Debug.Log(a.x);
        Debug.Log(a.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        //벽에 박으면 총알 터트리기
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Object_Rock"))
        {
            tearBoomAnim.SetTrigger("BoomTear");
        }
        //똥에 박으면
        else if (collision.gameObject.CompareTag("Object_Poop"))
        {
            tearBoomAnim.SetTrigger("BoomTear");
            collision.GetComponent<Poop>().GetDamage();
        }
        //불에 박으면
        else if (collision.gameObject.CompareTag("Object_Fire"))
        {
            tearBoomAnim.SetTrigger("BoomTear");
            collision.GetComponent<FirePlace>().GetDamage();
        }
        //적과 박으면
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            tearBoomAnim.SetTrigger("BoomTear");
            collision.gameObject.GetComponent<Enemy>().GetDamage(PlayerManager.instance.playerDamage);
            if (collision.gameObject.GetComponent<Enemy>())
            {

            }
            KnockBack();
        }
    }
}