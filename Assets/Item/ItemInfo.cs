using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class ItemInfo : MonoBehaviour
{
    protected int itemCode;
    public string itemTitle;
    public string itemDescription;
    public string itemInformation;

    public bool canCollision = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && canCollision
           && GameManager.instance.playerObject.GetComponent<PlayerController>().canChangeItem)
             {
            GameManager.instance.playerObject.GetComponent<PlayerController>().canChangeItem = false;
            // 아이템을 가지고있지않을때.
            if (!ItemManager.instance.PassiveItems[itemCode])
            {
                GameManager.instance.playerObject.GetComponent<PlayerController>().GetitemSound();

                UIManager.instance.ItemBanner(itemTitle, itemDescription);
                gameObject.layer = 31;
                ItemManager.instance.PassiveItems[itemCode] = true; // 미보유 -> 보유 로 변경 
                UseItem();

                gameObject.transform.SetParent(collision.gameObject.GetComponent<PlayerController>().itemPosition);
                gameObject.transform.localPosition = new Vector3(0, 0, 0);
                Destroy(gameObject.GetComponent<Rigidbody2D>());
                
                StartCoroutine(collision.gameObject.GetComponent<PlayerController>().GetItemTime());
            }
            Invoke("SetCanChangeItem", 1f);
        }
    }

    public virtual void UseItem() { Debug.Log("재정의 해줘!"); }

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
