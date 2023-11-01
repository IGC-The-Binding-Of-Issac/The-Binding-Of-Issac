using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Animations;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Tear : MonoBehaviour
{
    PlayerController playerController;
    Animator tearBoomAnim;
    Rigidbody2D tearRB;
    Vector3 tearPosition;
    Vector3 playerPosition;

    float betweenDistance;
    float playerTearSize;
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        tearBoomAnim = GetComponent<Animator>();
        tearRB = GetComponent<Rigidbody2D>();
        //플레이어 발사 시작위치
        playerPosition = playerController.transform.position;
    }

    void Update()
    {
        TearRange();
    }

    void TearRange()
    {
        //총알 위치
        tearPosition = this.transform.position;
        //둘 사이의 거리
        betweenDistance = Vector3.Distance(tearPosition, playerPosition);
        //둘 사이의 거리가 플레이어 사거리보다 커지면
        if (betweenDistance >= PlayerManager.instance.playerRange)
        {
            BoomTear();
        }
    }

    public void StopTear()
    {
        tearRB.velocity = Vector2.zero;
        //총알 오브젝트 속도를 zero로 만듬
    }

    public void BoomTear()
    {
        if (ItemManager.instance.PassiveItems[0] == true || (ItemManager.instance.ActiveItem != null && playerController.nailActivated))
        {
            tearBoomAnim.SetTrigger("RedBoomTear");
        }
        else
        {
            //눈물 터지는 애니메이션 실행
            tearBoomAnim.SetTrigger("BoomTear");
        }
    }
    public void TearSize()
    {
        playerTearSize = PlayerManager.instance.playerTearSize;
        gameObject.transform.localScale = new Vector3(playerTearSize, playerTearSize, 0);
    }

    public void DestoryTear()
    {
        //총알 파괴
        Destroy(gameObject);
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject tmp = GameManager.instance.playerObject.GetComponent<PlayerController>().CheckedObject;
        
        //벽에 박으면 총알 터트리기
        if (tmp != collision.gameObject) 
        {

            if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Object_Rock"))
            {
                gameObject.GetComponent<CircleCollider2D>().enabled = false;
                BoomTear();
            }

            //똥에 박으면
            else if (collision.gameObject.CompareTag("Object_Poop"))
            {
                gameObject.GetComponent<CircleCollider2D>().enabled = false;
                BoomTear();
                collision.GetComponent<Poop>().GetDamage();
            }
            //불에 박으면
            else if (collision.gameObject.CompareTag("Object_Fire"))
            {
                gameObject.GetComponent<CircleCollider2D>().enabled = false;
                BoomTear();
                collision.GetComponent<FirePlace>().GetDamage();
            }
            //적과 박으면
            else if (collision.gameObject.CompareTag("Enemy"))
            {
                gameObject.GetComponent<CircleCollider2D>().enabled = false;
                BoomTear();
                collision.gameObject.GetComponent<Enemy>().GetDamage(PlayerManager.instance.playerDamage);
                //Rigidbody2D enemyRB = collision.gameObject.GetComponent<Rigidbody2D>();
                //총알 방향
                Vector2 direction = gameObject.transform.GetComponent<Rigidbody2D>().velocity;
                //넉백
                StartCoroutine(collision.gameObject.GetComponent<Enemy>().knockBack());
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(direction * 200);
            }
        }
    }
}