using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBomb : MonoBehaviour
{
    private Animator gb;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gb = GetComponent<Animator>();

        float randomX = Random.Range(-1.0f, 1.0f);
        float randomY = Random.Range(-1.0f, 1.0f);
        float randomForce = Random.Range(50f, 70f);
        //최초 등장 시 (왼쪽, 오른쪽 / 위 , 아래) 방향을 랜덤으로 정해 움직이기 위한 AddForce
        rb.AddForce(new Vector2(randomX, randomY) * randomForce);

        // 최초 드랍 애니메이션 필요.
        //gb.SetTrigger("DropBomb");

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<AudioSource>().Play(); // 획득 사운드

            gameObject.layer = 31; // 플레이어와 충돌하지않는 레이어
            gb.SetTrigger("GetBomb");
            ItemManager.instance.bombCount++;
        }
    }


    //먹었을 때 애니메이션 이벤트 (사라지게 만들기)
    void GetBomb()
    {
        Destroy(gameObject);
    }
}
