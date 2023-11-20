using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBomb : MonoBehaviour
{
    private Animator gb;
    Sprite defaultSprite;

    // 상자에서 폭탄이 나옴과 동시에 획득하는 문제가 있어서
    // 드랍이후 획득까지 딜레이를 주기위한 변수입니다.
    bool getDelay;
    private void Start()
    {
        gb = GetComponent<Animator>();
        defaultSprite = GetComponent<SpriteRenderer>().sprite;
        getDelay = false;
    }

    public void DropBomb_move()
    {
        getDelay = false;
        StartCoroutine(GetDelay());
        float randomX = Random.Range(-1.0f, 1.0f);
        float randomY = Random.Range(-1.0f, 1.0f);
        float randomForce = Random.Range(50f, 70f);
        //최초 등장 시 (왼쪽, 오른쪽 / 위 , 아래) 방향을 랜덤으로 정해 움직이기 위한 AddForce
        GetComponent<Rigidbody2D>().AddForce(new Vector2(randomX, randomY) * randomForce);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && getDelay)
        {
            getDelay = false;
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
        getDelay = false;
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
        ItemManager.instance.itemTable.ReturnDropItem(gameObject);
    }
    IEnumerator GetDelay()
    {
        yield return new WaitForSeconds(0.3f);
        getDelay = true;
    }
}
