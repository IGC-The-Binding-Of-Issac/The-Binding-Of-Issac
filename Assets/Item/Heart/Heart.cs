using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    private CapsuleCollider2D cl;
    private float duration = 2;
    private float smoothness = 0.01f;

    bool getDelay;
    public void Start()
    {
        cl = GetComponent<CapsuleCollider2D>();
    }

    public void DropHeart()
    {
        float randomX = Random.Range(-1.0f, 1.0f);
        float randomY = Random.Range(-1.0f, 1.0f);
        float randomForce = Random.Range(50f, 70f);
        GetComponent<Rigidbody2D>().AddForce(new Vector2(randomX, randomY) * randomForce);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && PlayerManager.instance.playerHp < PlayerManager.instance.playerMaxHp)
        {
            gameObject.layer = 31;
            PlayerManager.instance.playerHp+=2;
            if (PlayerManager.instance.playerHp > PlayerManager.instance.playerMaxHp)
                PlayerManager.instance.playerHp = PlayerManager.instance.playerMaxHp;
            StartCoroutine(getHeart());
        }
        else if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Object_Fire")
            || collision.gameObject.CompareTag("Object_Poop") || collision.gameObject.CompareTag("Object_Rock"))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall")|| collision.gameObject.CompareTag("Object_Fire") 
            || collision.gameObject.CompareTag("Object_Poop") || collision.gameObject.CompareTag("Object_Rock"))
        {
            ContactPoint2D contactPoint = collision.GetContact(0);
            Vector2 direction = new Vector2(transform.position.x - contactPoint.point.x, transform.position.y - contactPoint.point.y);
            GetComponent<Rigidbody2D>().AddForce((direction).normalized * 35f);
        }
    }
    IEnumerator getHeart()
    {
        gameObject.GetComponent<AudioSource>().volume = SoundManager.instance.GetSFXVolume();
        gameObject.GetComponent<AudioSource>().Play(); // È¹µæ »ç¿îµå

        ResetObject();

        float progress = 0;
        float increment = smoothness / duration;
        while (progress < 0.65f)
        {
            transform.localScale += new Vector3(0.1f, -0.1f, 0);
            progress += increment;
            yield return new WaitForSeconds(smoothness);
            if (transform.localScale.y <= 0)
            {
                transform.localPosition = Vector3.zero;
                gameObject.SetActive(false);
                ItemManager.instance.itemTable.ReturnDropItem(gameObject);
            } 
        }
    }

    
    private void Update()
    {
        if (PlayerManager.instance.playerHp == PlayerManager.instance.playerMaxHp)
            cl.isTrigger = false;
   
        else cl.isTrigger = true;
    }

    public void ResetObject()
    {
        gameObject.layer = 14;
        transform.localScale = Vector3.one;
    }
}
