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

        transform.position = Vector3.MoveTowards(transform.position, bulletDesti, bulletSpeed * Time.deltaTime); //총알 움직임
        bulletDestroy(); // 도착하면 destory
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // 플레이어랑 닿이면 삭제
        {
            ani.SetBool("bulletDestroy" , true);
            PlayerManager.instance.GetDamage(); //플레이어랑 데미지
            Destroy(gameObject , waitForDest);
        }
        if (collision.gameObject.CompareTag("Wall")) // 벽 닿이면 삭제
        {
            ani.SetBool("bulletDestroy", true);
            Destroy(gameObject, waitForDest);
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
