using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalChest : MonoBehaviour
{
    [Header("Unity Setup")]
    [SerializeField] Sprite openChestSprite;
    [SerializeField] Sprite closeChestSprite;

    private void Start()
    {
        closeChestSprite = gameObject.GetComponent<SpriteRenderer>().sprite;
    }

    public void ResetObject()
    {
        // 초기화
        gameObject.GetComponent<SpriteRenderer>().sprite = closeChestSprite;
        gameObject.layer = 15;

        // 오브젝트 끄기.
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
            if(collision.gameObject.CompareTag("Player")) // 풀래아와 충돌시
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = openChestSprite; // 열린상자 이미지로 변경
                OpenChest(); // 드랍 아이템 생성
                openChestSound();
                StartCoroutine(StopChest()); // 밀린 오브젝트 멈추도기
            }
    }

    IEnumerator StopChest()
    {
        gameObject.layer = 16;
        yield return new WaitForSeconds(1.0f);
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

    }

    void OpenChest()
    {
        for (int i = 0; i < 2; i++)
        {
            int rd = Random.Range(0, 4);
            if (rd == 0)
            {
                int coin = Random.Range(0, 6);
                for (int j = 0; j < coin; j++)
                {
                    ItemManager.instance.itemTable.Dropitem(transform.position, 0);
                }
            }
            else
            {
                ItemManager.instance.itemTable.Dropitem(transform.position, rd);
            }
        }
    }

    void openChestSound()
    {
        gameObject.GetComponent<AudioSource>().Play();
    }
}
