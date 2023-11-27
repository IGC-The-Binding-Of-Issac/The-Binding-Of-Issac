using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldChest : Chest
{
    protected override void initialization()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = closeSprite;
    }

    public override void Returnobject()
    {
        GameManager.instance.roomGenerate.GoldChestPool.Push(gameObject);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && ItemManager.instance.keyCount > 0)
        {
            ItemManager.instance.keyCount--; // ¿­¼è »ç¿ë
            OpenChest();
        }
    }

    protected override void DropReward()
    {
        GameObject it = Instantiate(ItemManager.instance.itemTable.OpenGoldChest(), transform.position, Quaternion.identity) as GameObject;
        GameManager.instance.roomGenerate.itemList.Add(it);
    }
}
