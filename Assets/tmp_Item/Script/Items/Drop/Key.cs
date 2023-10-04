using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private Animator key_ani;
    private Rigidbody2D rb;
    private void Start()
    {
        key_ani = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        float randomX = Random.Range(-1.0f, 1.0f);
        float randomY = Random.Range(-1.0f, 1.0f);
        float randomForce = Random.Range(50f, 70f);
        rb.AddForce(new Vector2(randomX, randomY) * randomForce);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            key_ani.SetTrigger("GetKey");
            ItemManager.instance.keyCount++;
        }
    }
    private void GetKey()
    {
        Destroy(gameObject);
    }
}
