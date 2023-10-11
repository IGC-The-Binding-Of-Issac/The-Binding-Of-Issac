using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    Animator coin_animator;
    private Rigidbody2D rb;
    private CapsuleCollider2D cl;
    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        float randomX = Random.Range(-1.0f, 1.0f);
        float randomY = Random.Range(-1.0f, 1.0f);
        float randomForce = Random.Range(50f, 70f);
        rb.AddForce(new Vector2(randomX, randomY) * randomForce);
        coin_animator = GetComponent<Animator>();
        cl = GetComponent<CapsuleCollider2D>();
        cl.enabled = false;
    }

    public void OnTriggerEnter2D(Collider2D Collision)
    {
        if (Collision.CompareTag("Player"))
        {
            coin_animator.SetTrigger("GetCoin");
            ItemManager.instance.coinCount++;
        }
    }

    public void GetCoin()
    {
        Destroy(this.gameObject);
    }
    public void DropCoinEnd()
    {
        coin_animator.SetBool("DropCoin", false);
        cl.enabled = true;
    }
}
