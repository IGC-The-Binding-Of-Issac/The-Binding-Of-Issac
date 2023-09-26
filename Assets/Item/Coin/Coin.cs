using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    Animator coin_animator;
    public void Start()
    {
        coin_animator = GetComponent<Animator>();
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
    }
}
