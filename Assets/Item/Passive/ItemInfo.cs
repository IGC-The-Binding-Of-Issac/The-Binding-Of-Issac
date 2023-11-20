using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class ItemInfo : MonoBehaviour
{
    [Header("int")]
    protected int itemCode;           //패시브 아이템 고유 코드

    [Header("string")]
    public string itemTitle;         
    public string itemDescription;
    public string itemInformation;

    [Header("bool")]
    public bool canCollision = false; //아이템 충돌 가능 여부
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && canCollision
           && GameManager.instance.playerObject.GetComponent<PlayerController>().canChangeItem) //아이템 습득 가능 시
             {
            GameManager.instance.playerObject.GetComponent<PlayerController>().canChangeItem = false;
            
            //1. 패시브 아이템 최초 습득 시
            if (!ItemManager.instance.PassiveItems[itemCode])
            {
                GameManager.instance.playerObject.GetComponent<PlayerController>().GetitemSound();
                UIManager.instance.ItemBanner(itemTitle, itemDescription);
                gameObject.layer = 31;
                ItemManager.instance.PassiveItems[itemCode] = true; // 미보유 -> 보유 로 변경 
                UseItem(); //습득 시 효과 발생
                gameObject.transform.SetParent(collision.gameObject.GetComponent<PlayerController>().itemPosition);
                gameObject.transform.localPosition = new Vector3(0, 0, 0);
                Destroy(gameObject.GetComponent<Rigidbody2D>());
                StartCoroutine(collision.gameObject.GetComponent<PlayerController>().GetItemTime());
            }
            Invoke("SetCanChangeItem", 1f);
        }
    }

    //패시브 아이템 습득 시 효과 재정의
    public virtual void UseItem() 
    {

    }

    public void SetItemCode(int code)
    {
        itemCode = code;
    }
    void SetCanChangeItem()
    {
        GameManager.instance.playerObject.GetComponent<PlayerController>().canChangeItem = true;
    }
    public void SetItemString(string title, string description, string information)
    {
        itemTitle = title;
        itemDescription = description;
        itemInformation = information;
    }
    void SetDelay()
    {
        canCollision = true;
    }
    private void Update()
    {
        if (!canCollision)
            Invoke("SetDelay", 0.8f);
    }
}
