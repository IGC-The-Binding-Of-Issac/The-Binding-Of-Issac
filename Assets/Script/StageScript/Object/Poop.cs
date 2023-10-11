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
        if(poopIndex >= 3) // 체력이 전부 깍이면
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false; //콜라이더 없애기.
            ItemDrop();
        }
    }

    void ChangeSprite()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = poopSprite[poopIndex];
    }

    void ItemDrop()
    {
        int rd = Random.Range(0, 3);
        if (rd == 0)
        {
            GameObject it = Instantiate(ItemManager.instance.itemTable.ObjectBreak(), transform.position, Quaternion.identity) as GameObject;
        }
    }
}
