using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePlace : MonoBehaviour
{
    int fireIndex = -1;

    [Header("Unity SetUp")]
    [SerializeField] Sprite woodSprite;
    [SerializeField] GameObject eft;

    public void GetDamage()
    {
        fireIndex++;
        ChangeEffectSize();
        if(fireIndex == 3)
        {
            ItemDrop();
            eft.SetActive(false);
            gameObject.GetComponent<SpriteRenderer>().sprite = woodSprite;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void ChangeEffectSize()
    {
        eft.transform.localScale = new Vector3(eft.transform.localScale.x - 0.2f, eft.transform.localScale.y - 0.2f, 0);
    }
    private void ItemDrop()
    {
        // ItemManage에서 구현후 실행 고민해봅시다.
        //코인,체력 등 드랍아이템 확률드랍. 구현 필요.
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerManager.instance.GetDamage();
        }
        // 몬스터에게 데미지주기 작성 필요.
    }
}
