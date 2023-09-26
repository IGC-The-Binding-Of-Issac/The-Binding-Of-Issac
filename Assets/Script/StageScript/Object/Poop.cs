using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poop : MonoBehaviour
{
    int poopIndex = -1;

    [Header("Unity SetUp")]
    [SerializeField] Sprite[] poopSprite;
    public void GetDamage()
    {
        poopIndex++;
        ChangeSprite();
        if(poopIndex == 3) // 체력이 전부 깍이면
        {
            ItemDrop();
            gameObject.GetComponent<BoxCollider2D>().enabled = false; //콜라이더 없애기.
        }
    }

    void ChangeSprite()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = poopSprite[poopIndex];
    }

    void ItemDrop()
    {
        // ItemManage에서 구현후 실행 고민해봅시다.
        //코인,체력 등 드랍아이템 확률드랍. 구현 필요.
    }
}
