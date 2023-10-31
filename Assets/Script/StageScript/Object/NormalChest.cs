using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalChest : MonoBehaviour
{
    [Header("Unity Setup")]
    [SerializeField] Sprite openChestSprite;

    private void OnCollisionEnter2D(Collision2D collision)
    {
            if(collision.gameObject.CompareTag("Player")) // 풀래아와 충돌시
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = openChestSprite; // 열린상자 이미지로 변경
                OpenChest(); // 드랍 아이템 생성
                StartCoroutine(StopChest());
            }
    }

    IEnumerator StopChest()
    {
        gameObject.layer = 16;
        yield return new WaitForSeconds(1.0f);
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        Destroy(gameObject.GetComponent<Rigidbody2D>());
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
                    GameObject it = Instantiate(ItemManager.instance.itemTable.OpenNormalChest(rd), transform.position, Quaternion.identity) as GameObject;
                    GameManager.instance.roomGenerate.itemList.Add(it);
                }
            }
            else
            {
                GameObject it = Instantiate(ItemManager.instance.itemTable.OpenNormalChest(rd), transform.position, Quaternion.identity) as GameObject;
                GameManager.instance.roomGenerate.itemList.Add(it);
            }
        }
    }
}
