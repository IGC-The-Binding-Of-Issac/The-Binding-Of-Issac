using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalChest : MonoBehaviour
{
    [Header("Unity Setup")]
    [SerializeField] Sprite openChestSprite;

    bool chestState = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player")) // 풀래아와 충돌시
        {
            if(chestState) // 이미 열린상자인지 체크 
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = openChestSprite; // 열린상자 이미지로 변경
                chestState = false; // 열린상태로 전환
                OpenChest(); // 드랍 아이템 생성
            }
        }
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
                }
            }
            else
            {
                GameObject it = Instantiate(ItemManager.instance.itemTable.OpenNormalChest(rd), transform.position, Quaternion.identity) as GameObject;
            }
        }
    }
}
