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
        ChangeSprite(poopIndex);
        if(poopIndex >= 3) // 체력이 전부 깍이면
        {
            DestorySound();
            gameObject.GetComponent<BoxCollider2D>().enabled = false; //콜라이더 없애기.
            ItemDrop();
        }
    }

    void ChangeSprite(int index)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = poopSprite[index];
    }

    void ItemDrop()
    {
        int rd = Random.Range(0, 3);
        if (rd <= 0)
        {
            GameObject it = Instantiate(ItemManager.instance.itemTable.ObjectBreak(), transform.position, Quaternion.identity) as GameObject;
            GameManager.instance.roomGenerate.itemList.Add(it);
            
        }
    }

    void DestorySound()
    {
        gameObject.GetComponent<AudioSource>().Play();
    }
}
