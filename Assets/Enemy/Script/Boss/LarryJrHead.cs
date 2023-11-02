using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LarryJrHead : MonoBehaviour
{
    SnakeManager parent;

    void Start()
    {
        parent = transform.parent.GetComponent<SnakeManager>();
    }

    /// <summary>
    /// 눈물, player 충돌
    /// 
    /// 1. 부모에 rigidbody를 들고옴
    /// 2. 자식이 충돌해도 밑의 충돌처리로 해결 가능
    /// 
    /// </summary>

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Larry 몸 + 머리 , 눈물 충돌
        if (collision.gameObject.CompareTag("Tears"))
        {
            parent.getDamageLarry();
        }
        if (collision.gameObject.CompareTag("AttackBomb")) 
        {
            // 충돌한 오브젝트 (폭탄)의 데미지를 가져옴
            float damage;
            damage = collision.gameObject.GetComponent<PutBomb>().retunbossBombDamage();
            parent.getBombDamage(damage);

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //플레이어와 충돌 했을 때 (자식포함)
        if (collision.gameObject.CompareTag("Player"))
        {
            parent.hitDamagePlayer();
        }
    }


}
