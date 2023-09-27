using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PutBomb : MonoBehaviour
{
    public Animator bombAnimator;
    public SpriteRenderer bombSpr;
    public Rigidbody2D rb;
    public BoxCollider2D bc;
   
    private float duration = 2;
    private float smoothness = 0.01f;

    bool CanAttack;
    void Start()
    {
        CanAttack = false;
        bombAnimator = GetComponent<Animator>();
        bombSpr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();

        PlayerBomb();
    }

    public void PlayerBomb()
    {
        StartCoroutine(Boom());
        bombAnimator.SetTrigger("IsBomb");
    }
    public IEnumerator Boom()
    {

        float progress = 0;
        float increment = smoothness / duration;
        while (progress < 0.823f)
        {
            bombSpr.color = Color.Lerp(Color.white, Color.red, progress);
            progress += increment;
            yield return new WaitForSeconds(smoothness);
        }
        bombSpr.color = Color.white;
    }


    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            this.GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(CanAttack)
        {
            if(collision.CompareTag("Object_Rock"))
            {
                collision.GetComponent<Rock>().DestroyRock();
            }
            if(collision.CompareTag("Object_Poop"))
            {
                for(int i = 0; i < 4; i++)
                    collision.GetComponent<Poop>().GetDamage();
            }
            if(collision.CompareTag("Object_Fire"))
            {
                for (int i = 0; i < 4; i++)
                    collision.GetComponent<FirePlace>().GetDamage();
            }
            if(collision.CompareTag("Enemy"))
            {
                // 터졌을때 적에게 데미지 100 죽기.
            }
            if(collision.CompareTag("Player"))
            {
                // 터졌을때 플레이어에게 데미지 주기.
                PlayerManager.instance.GetDamage();
            }
        }
    }

    public void BombDelete()
    {
        Destroy(this.gameObject);
    }

    public void BombAttack()
    {
        transform.position += new Vector3(0, 1.3f, 0);

        bc.isTrigger = true;
        CanAttack = true;
        bc.offset = new Vector2(0, -0.25f);
        bc.size = new Vector2(1.81f, 2.31f);
    }
}