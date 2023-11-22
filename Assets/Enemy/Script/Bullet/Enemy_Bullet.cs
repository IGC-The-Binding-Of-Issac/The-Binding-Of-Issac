using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Bullet : MonoBehaviour
{
    protected Animator ani;
    protected float bulletSpeed;
    protected float waitForDest;
    protected bool isCoru;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // 플레이어랑 닿이면 삭제
        {
            ani.SetBool("bulletDestroy", true);
            PlayerManager.instance.GetDamage(); //플레이어랑 데미지

        }
        else if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Object_Rock")) // 벽 닿이면 삭제
        {
            ani.SetBool("bulletDestroy", true);
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;

            //StartCoroutine(EnemyPooling.Instance.waitReturn(waitForDest, this.gameObject)); // pooling return
        }

        //똥에 박으면
        else if (collision.gameObject.CompareTag("Object_Poop"))
        {
            ani.SetBool("bulletDestroy", true);
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;

            //StartCoroutine(EnemyPooling.Instance.waitReturn(waitForDest, this.gameObject)); // pooling return
            collision.gameObject.GetComponent<Poop>().GetDamage();
        }
        //불에 박으면
        else if (collision.gameObject.CompareTag("Object_Fire"))
        {
            ani.SetBool("bulletDestroy", true);
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;

            //StartCoroutine(EnemyPooling.Instance.waitReturn(waitForDest, this.gameObject)); // pooling return
            collision.gameObject.GetComponent<FirePlace>().GetDamage();
        }

        //돌에 박으면
        else if (collision.gameObject.CompareTag("Object_Rock"))
        {
            ani.SetBool("bulletDestroy", true);
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;

            //StartCoroutine(EnemyPooling.Instance.waitReturn(waitForDest, this.gameObject)); // pooling return
        }
    }

    public void resetBullet()
    {
        Debug.Log("이에ㅐㅇㅇ");
        EnemyPooling.Instance.returnBullet(this.gameObject);
    }
}
