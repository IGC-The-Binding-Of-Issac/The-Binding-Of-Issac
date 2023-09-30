using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBomb : MonoBehaviour
{
    private Animator gb;
    private Rigidbody2D rb;
    private int[] direction = { -1, 1 };
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gb = GetComponent<Animator>();

        int randomX = Random.Range(0, 2);
        int randomY = Random.Range(0, 2);
        //최초 등장 시 (왼쪽, 오른쪽 / 위 , 아래) 방향을 랜덤으로 정해 움직이기 위한 AddForce
        rb.AddForce(new Vector2(direction[randomX], direction[randomY]), ForceMode2D.Impulse);

        // 최초 드랍 애니메이션 필요.
        //gb.SetTrigger("DropBomb");

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
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
