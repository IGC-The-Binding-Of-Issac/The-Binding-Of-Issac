using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBomb : MonoBehaviour
{
    private Animator gb;
    public Sprite defaultSprite;

    private void Start()
    {
        gb = GetComponent<Animator>();
    }

    public void DropBomb_move()
    {
        float randomX = Random.Range(-1.0f, 1.0f);
        float randomY = Random.Range(-1.0f, 1.0f);
        float randomForce = Random.Range(50f, 70f);
        //최초 등장 시 (왼쪽, 오른쪽 / 위 , 아래) 방향을 랜덤으로 정해 움직이기 위한 AddForce
        GetComponent<Rigidbody2D>().AddForce(new Vector2(randomX, randomY) * randomForce);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<AudioSource>().volume = SoundManager.instance.GetSFXVolume();
            gameObject.GetComponent<AudioSource>().Play(); // 획득 사운드

            gameObject.layer = 31; // 플레이어와 충돌하지않는 레이어
            gb.SetTrigger("GetBomb");
            ItemManager.instance.bombCount++;
        }
    }


    //먹었을 때 애니메이션 이벤트 (사라지게 만들기)
    public void GetBomb()
    {
        transform.localPosition = Vector3.zero;
        StartCoroutine(Delay());
    }


    public void ResetObject()
    {
        GetComponent<SpriteRenderer>().sprite = defaultSprite;
        gameObject.layer = 14;
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
        ItemManager.instance.itemTable.ReturnDropItem(gameObject);
    }
}
