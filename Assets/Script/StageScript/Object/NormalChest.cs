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
        GameObject coin;
        coin = ItemManager.instance.itemTable.GetDropItem(0); // 코인 받아오기
        coin.transform.position = gameObject.transform.position;
        coin.GetComponent<Coin>().DropCoin();

        // Before
        //for (int i = 0; i < 2; i++)
        //{
        //    int rd = Random.Range(0, 4);
        //    if (rd == 0)
        //    {
        //        int coin = Random.Range(0, 6);
        //        for (int j = 0; j < coin; j++)
        //        {
        //            GameObject it = Instantiate(ItemManager.instance.itemTable.OpenNormalChest(rd), transform.position, Quaternion.identity) as GameObject;
        //            GameManager.instance.roomGenerate.itemList.Add(it);
        //        }
        //    }
        //    else
        //    {
        //        GameObject it = Instantiate(ItemManager.instance.itemTable.OpenNormalChest(rd), transform.position, Quaternion.identity) as GameObject;
        //        GameManager.instance.roomGenerate.itemList.Add(it);
        //    }
        //}
    }

    void openChestSound()
    {
        gameObject.GetComponent<AudioSource>().Play();
    }
}
