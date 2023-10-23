using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    Animator ani;
    [SerializeField] Vector3 bulletDesti;
    [SerializeField] Transform playerPosi;

    float bulletSpeed;
    float waitForDest;


    bool isMoving = true;
    private void Start()
    {
        ani = GetComponent<Animator>();
        playerPosi = GameObject.FindWithTag("Player").transform; //플레이어의 위치
        bulletDesti = new Vector3(playerPosi.position.x, playerPosi.position.y, 0);

        waitForDest = 0.5f;
        bulletSpeed = 5f;
    }

    private void Update()
    {
        if(isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, bulletDesti, bulletSpeed * Time.deltaTime); //총알 움직임
            bulletDestroy(); // 도착하면 destory
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // 플레이어랑 닿이면 삭제
        {
            isMoving = false;
            ani.SetBool("bulletDestroy", true);
            PlayerManager.instance.GetDamage(); //플레이어랑 데미지
            Destroy(gameObject, waitForDest);
        }
        else if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Object_Rock")) // 벽 닿이면 삭제
        {
            isMoving = false;
            ani.SetBool("bulletDestroy", true);
            Destroy(gameObject, waitForDest);
        }

        //똥에 박으면
        else if (collision.gameObject.CompareTag("Object_Poop"))
        {
            isMoving = false;
            ani.SetBool("bulletDestroy", true);
            Destroy(gameObject, waitForDest);
            collision.gameObject.GetComponent<Poop>().GetDamage();
        }
        //불에 박으면
        else if (collision.gameObject.CompareTag("Object_Fire"))
        {
            isMoving = false;
            ani.SetBool("bulletDestroy", true);
            Destroy(gameObject, waitForDest);
            collision.gameObject.GetComponent<FirePlace>().GetDamage();
        }
    }

    // 초기에 설정된 거리에 도착하면 Destory
    void bulletDestroy() 
    {
        if (Vector3.Distance(gameObject.transform.position , bulletDesti) <= 0.5f) 
        {
            ani.SetBool("bulletDestroy", true);
            Destroy(gameObject, waitForDest);
        }
    }
}
