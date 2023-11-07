using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenPoop : MonoBehaviour
{
    [SerializeField]
    Sprite[] goldenPoopSpr;
    int startIdx = -1;

    Collider2D col;
    private void Awake()
    {
        col = gameObject.GetComponent<Collider2D>();
        col.isTrigger = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            col.isTrigger = false;
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //   if (collision.gameObject.CompareTag("Tears"))
    //    {
    //        GetDamage();
    //    }
    //}
    public void GetDamage()
    {
        startIdx++;
        gameObject.GetComponent<SpriteRenderer>().sprite = goldenPoopSpr[startIdx];
        if (startIdx == 3)
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
            DestorySound();
        }
    }
    void DestorySound()
    {
        gameObject.GetComponent<AudioSource>().Play();
    }
}
