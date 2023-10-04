using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Heart : MonoBehaviour
{
    private Rigidbody2D rb;
    private CapsuleCollider2D heartCollider;
    private float duration = 2;
    private float smoothness = 0.01f;

    bool canUse = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        float randomX = Random.Range(-1.0f, 1.0f);
        float randomY = Random.Range(-1.0f, 1.0f);
        float randomForce = Random.Range(50f, 70f);
        rb.AddForce(new Vector2(randomX, randomY) * randomForce);
        heartCollider = GetComponent<CapsuleCollider2D>();
    }
    private void Update()
    {
        if(canUse)
        {
            if (PlayerManager.instance.playerHp >= PlayerManager.instance.playerMaxHp)
            {
                heartCollider.isTrigger = false;
            }
            //체력이 풀피이면 하트를 먹어도 밀리게끔 트리거 해제
            else if (PlayerManager.instance.playerHp < PlayerManager.instance.playerMaxHp)
            {
                heartCollider.isTrigger = true;
            }
        }
        //체력이 닳아있으면 하트를 먹으면 체력이 회복되도록 트리거 설정
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canUse = false;
            if (PlayerManager.instance.playerHp + 2 >= PlayerManager.instance.playerMaxHp) PlayerManager.instance.playerHp = PlayerManager.instance.playerMaxHp;
            else PlayerManager.instance.playerHp += 2;
            StartCoroutine(getHeart());
        }
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Object_Poop")
            || collision.gameObject.CompareTag("Object_Rock"))
        {
            rb.velocity = new Vector2(0,0);
        }
    }

    public IEnumerator getHeart()
    {
        float progress = 0;
        float increment = smoothness / duration;
        while (progress < 0.3f && transform.localScale.y > 0)
        {
            transform.localScale += new Vector3(0.15f, -0.1f, 0);
            progress += increment;
            yield return new WaitForSeconds(smoothness);
        }
        if (transform.localScale.y < 0)
        {
            Destroy(gameObject);
        }
    }
}
