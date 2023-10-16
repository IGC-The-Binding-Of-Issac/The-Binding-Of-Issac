using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    private Rigidbody2D rb;
    private CapsuleCollider2D cl;
    private float duration = 2;
    private float smoothness = 0.01f;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        float randomX = Random.Range(-1.0f, 1.0f);
        float randomY = Random.Range(-1.0f, 1.0f);
        float randomForce = Random.Range(50f, 70f);
        rb.AddForce(new Vector2(randomX, randomY) * randomForce);

        cl = GetComponent<CapsuleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && PlayerManager.instance.playerHp < PlayerManager.instance.playerMaxHp)
        {
            gameObject.layer = 31;
            PlayerManager.instance.playerHp++;
            StartCoroutine(getHeart());
        }
        else if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Object_Fire")
            || collision.gameObject.CompareTag("Object_Poop") || collision.gameObject.CompareTag("Object_Rock"))
        {
            rb.velocity = new Vector2(0, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall")|| collision.gameObject.CompareTag("Object_Fire") 
            || collision.gameObject.CompareTag("Object_Poop") || collision.gameObject.CompareTag("Object_Rock"))
        {
            ContactPoint2D contactPoint = collision.GetContact(0);
            Vector2 direction = new Vector2(transform.position.x - contactPoint.point.x, transform.position.y - contactPoint.point.y);
            rb.AddForce((direction).normalized * 35f);
        }
    }
    IEnumerator getHeart()
    {
        float progress = 0;
        float increment = smoothness / duration;
        while (progress < 0.65f)
        {
            transform.localScale += new Vector3(0.1f, -0.1f, 0);
            progress += increment;
            yield return new WaitForSeconds(smoothness);
            if (transform.localScale.y <= 0)
            {
                Destroy(gameObject);
            } 
        }
    }

    private void Update()
    {
        if (PlayerManager.instance.playerHp == PlayerManager.instance.playerMaxHp)
            cl.isTrigger = false;
        
        else cl.isTrigger = true;
    }
}
