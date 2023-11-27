using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalChest : Chest
{
    protected override void initialization()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = closeSprite;
    }

    public override void Returnobject()
    {
        GameManager.instance.roomGenerate.NormalChestPool.Push(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            OpenChest();
        }
    }

    protected override void DropReward()
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
}
