 using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class PutBomb : MonoBehaviour
{
    public Animator bombAnimator;
    public SpriteRenderer bombSpr;
    public Rigidbody2D rb;
    public BoxCollider2D bc;
   
    private float duration = 2;
    private float smoothness = 0.01f;
    private float bossBombDamage = 30f;

    bool CanAttack;

    public AudioClip explosionClip;
    public AudioClip putClip;
    void Awake()
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
        // 설치 Sound 실행
        gameObject.GetComponent<AudioSource>().volume = SoundManager.instance.GetSFXVolume();
        gameObject.GetComponent<AudioSource>().clip = putClip;
        gameObject.GetComponent<AudioSource>().Play();

        StartCoroutine(Boom());
        bombAnimator.SetTrigger("IsBomb");
    }
    public IEnumerator Boom()
    {

        float progress = 0;
        float increment = smoothness / duration;
        while (progress < 0.7f)
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
            else if (collision.gameObject.name == "Golden Poop(Clone)")
            {
                for (int i = 0; i < 4; i++)
                    collision.GetComponent<GoldenPoop>().GetDamage();
            }
            if (collision.CompareTag("Object_Fire"))
            {
                for (int i = 0; i < 4; i++)
                    collision.GetComponent<FirePlace>().GetDamage();
            }
            if(collision.CompareTag("Enemy"))
            {
                collision.GetComponent<TEnemy>().GetDamage(100);   
            }
            if(collision.CompareTag("Player"))
            {
                if (ItemManager.instance.PassiveItems[16]) return;
                else PlayerManager.instance.GetDamage();

            }
        }
    }

    public void BombDelete()
    {
        Destroy(this.gameObject);
    }

    public void BombAttack()
    {
        // 폭파 Sound 실행
        gameObject.GetComponent<AudioSource>().volume = SoundManager.instance.GetSFXVolume();
        gameObject.GetComponent<AudioSource>().clip = explosionClip;
        gameObject.GetComponent<AudioSource>().Play();

        transform.position += new Vector3(0, 1f, 0);

        bc.isTrigger = true;
        CanAttack = true;
        bc.offset = new Vector2(0, -0.2f);
        bc.size = new Vector2(2.5f, 2.5f);
    }

    //메서드 추가
    public float retunbossBombDamage() 
    {
        return bossBombDamage;
    }
}