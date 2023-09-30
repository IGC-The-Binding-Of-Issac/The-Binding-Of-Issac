using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tear : MonoBehaviour
{

    PlayerController playerController;
    Animator tearBoomAnim;

    Vector3 tearPosition;
    Vector3 playerPosition;

    float betweenDistance;
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        tearBoomAnim = GetComponent<Animator>();
    }

    void Update()
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

    public void DestoryTear()
    {
        //총알 파괴
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //벽에 박으면 총알 터트리기
        if(collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Object_Rock"))
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
        //else if(collision.gameObject.CompareTag("Enemy"))
        //{
        //    tearBoomAnim.SetTrigger("BoomTear");
        //    // 대충 적에게 데미지 주기 코드 작성 바람.
        //}
    }
}