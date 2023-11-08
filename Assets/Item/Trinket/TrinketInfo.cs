using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrinketInfo : MonoBehaviour
{
    public int trinketItemCode;
    public string itemTitle; //아이템 이름
    public string itemDescription; //아이템 요약 설명 [습득 시 중앙 UI 밑에 텍스트 한줄]
    public string itemInformation; // 아이템 설명 [습득 전 왼쪽 UI에 설명들]
    public bool canCollision = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌 대상이 플레이어일때
        if(collision.gameObject.CompareTag("Player") && canCollision
            && GameManager.instance.playerObject.GetComponent<PlayerController>().canChangeItem)
        {
            gameObject.layer = 31;

            // 1. 장신구 아이템을 장착하고 있지 않을 시
            if (ItemManager.instance.TrinketItem == null)
            {
                UIManager.instance.ItemBanner(itemTitle, itemDescription);
                GameManager.instance.playerObject.GetComponent<PlayerController>().canChangeItem = false;
                TrinketGet(collision);
            }
            
            // 2. 장신구 아이템을 장착하고 있을 시
            else if(ItemManager.instance.TrinketItem != null)
            {
                UIManager.instance.ItemBanner(itemTitle, itemDescription);
                GameManager.instance.playerObject.GetComponent<PlayerController>().canChangeItem = false;
                // 현재 보유중인 아이템의 효과를 제거.
                ItemManager.instance.TrinketItem.GetComponent<TrinketInfo>().DropTrinket();

                // 현재 보유중인 아이템을 생성
                GameObject obj = ItemManager.instance.itemTable.TrinketChange(ItemManager.instance.TrinketItem.GetComponent<TrinketInfo>().trinketItemCode);
                Transform dropPosition = GameManager.instance.playerObject.GetComponent<PlayerController>().itemPosition;
                GameObject beforeTrinket = Instantiate(obj, new Vector3(dropPosition.position.x, (dropPosition.position.y - 1f), 0),Quaternion.identity) as GameObject;

                // 현재 드랍된 아이템 리스트에 등록.
                GameManager.instance.roomGenerate.itemList.Add(beforeTrinket);

                // 원래있던 아이템 제거.
                Destroy(ItemManager.instance.TrinketItem);
                TrinketGet(collision);
            }
            Invoke("SetCanChangeItem", 1f);
        }
    }

    public void SetTrinketItemCode(int code)
    {
        trinketItemCode = code;
    }

    public void SetCanChangeItem()
    {
        GameManager.instance.playerObject.GetComponent<PlayerController>().canChangeItem = true;
    }
    public void SetTrinketString(string title, string description, string information)
    {
        itemTitle = title;
        itemDescription = description;
        itemInformation = information;
    }
    public void KeepItem() 
    {
        transform.position = ItemManager.instance.itemStorage.position;
        transform.SetParent(ItemManager.instance.itemStorage);
    }

    public virtual void GetItem() { 
        Debug.Log("재정의 해줘!"); 
    }

    public virtual void DropTrinket()
    {
        Debug.Log("장신구 떨어뜨릴 때 재정의");
    }
    private void TrinketChange(Collision2D collision)
    {
        ItemManager.instance.TrinketItem = this.gameObject;
        DisconnectTrinket();
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

        gameObject.transform.SetParent(collision.gameObject.GetComponent<PlayerController>().itemPosition);
        gameObject.transform.localPosition = new Vector3(0, 0, 0);

        Destroy(gameObject.GetComponent<Rigidbody2D>());
        StartCoroutine(collision.gameObject.GetComponent<PlayerController>().GetTrinketItem());

    }
    private void TrinketGet(Collision2D collision)
    {
        TrinketChange(collision);
        GetItem();
    }

    void DisconnectTrinket()
    {
        /*
         방이 새로 생성될때 보유중인 Trinket도 같이 삭제되는현상을 해결하기위해
         현재 보유중인 Trinket은 방 생성 스크립트의 itemList에서 제외함으로써 ( 방에 드랍되는 모든 아이템들은 ItemList로 들어감 )
         방이 재 생성될때 ( 스테이지 넘어갈때 )  보유중인 장신구에 아무런 영향이 가지못하도록 수정함.
        */
        // roomGenerete의 itemList에서 제외시킴.
        for (int i = 0; i < GameManager.instance.roomGenerate.itemList.Count; i++)
        {
            if (ItemManager.instance.TrinketItem.Equals(GameManager.instance.roomGenerate.itemList[i]))
            {
                GameManager.instance.roomGenerate.itemList.RemoveAt(i);
            }
        }
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
