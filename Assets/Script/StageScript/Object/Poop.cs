using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Poop : Obstacle
{
    [Header("Unity SetUp")]
    [SerializeField] Sprite[] poopSprite;

    protected override void initialization()
    {
        GetComponent<SpriteRenderer>().sprite = poopSprite[spriteIndex];
        objectLayer = 7;
    }

    public override void ResetObject()
    {
        // 초기화
        spriteIndex = 0;
        GetComponent<SpriteRenderer>().sprite = poopSprite[0];
        gameObject.layer = objectLayer;

        // 오브젝트 끄기.
        gameObject.SetActive(false);
    }

    public override void Returnobject()
    {
        GameManager.instance.roomGenerate.PoopPool.Push(gameObject);
    }

    public override void GetDamage()
    {
        if (spriteIndex > 4)
            return;

        spriteIndex++;
        ChangeObjectSPrite();
        if(spriteIndex >= 4) // 체력이 전부 깍이면
        {
            gameObject.layer = noCollisionLayer;

            DestorySound();

            DropItem();
        }
    }

    protected override void ChangeObjectSPrite()
    {
        if (spriteIndex >= 4)
            spriteIndex = 4;

        gameObject.GetComponent<SpriteRenderer>().sprite = poopSprite[spriteIndex];
    }

    protected override void DropItem()
    {
        int rd = Random.Range(0, 3);
        if (rd <= 0)
        {
            ItemManager.instance.itemTable.Dropitem(transform.position, rd);
        }
    }

}
