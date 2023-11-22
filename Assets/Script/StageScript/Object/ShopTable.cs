using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ShopTable : MonoBehaviour
{
    GameObject roomObject;
    Room roomInfo;
    [Header("State")]
    [SerializeField] int cost;
    [SerializeField] GameObject item;
    public string[] itemInfomation = new string[2];

    [Header("Unty Setup")]
    [SerializeField] Transform itemPos;
    [SerializeField] GameObject Cost_10;
    [SerializeField] GameObject Cost_1;
    public Sprite[] costImages;
    private void FixedUpdate()
    {
        if(roomInfo != null)
        {
            // 플레이어가 방에 들어왔을때.
            if(roomInfo.playerInRoom)
            {
                int mode = Random.Range(0, 1000);
                SetShopItem(mode%3);

                roomInfo = null;
            }
        }

        if(item != null)
        {
            ItemLayer();
        }
    }

    public void ResetObject()
    {
        roomObject = null;
        roomInfo = null;
        cost = 0;
        if(item != null)
        {
            // 드랍아이템인지 확인
            // 드랍아이템이라면 오브젝트풀로 돌려주고 
            // 그외 아이템이라면 삭제
            if (CheckedDropItem(item))
            {
                item.GetComponent<Collider2D>().enabled = true; // 콜라이더 on 
                ItemManager.instance.itemTable.ReturnDropItem(item);
            }
            else
                Destroy(item);

            item = null;
        }
        itemInfomation[0] = "";
        itemInfomation[1] = "";

        Cost_1.GetComponent<SpriteRenderer>().sprite = costImages[0];
        Cost_10.GetComponent<SpriteRenderer>().sprite = costImages[0];

        gameObject.SetActive(false);

        gameObject.layer = 14;
    }

    bool CheckedDropItem(GameObject obj)
    {
        if (obj.GetComponent<key>() != null || obj.GetComponent<Heart>() != null || obj.GetComponent<DropBomb>() != null)
            return true;
        return false;
    }

    void ItemLayer()
    {
        if (GameManager.instance.playerObject.transform.position.y > gameObject.transform.position.y)
        {
            item.GetComponent<SpriteRenderer>().sortingOrder = 110;
        }
        else
        {
            item.GetComponent<SpriteRenderer>().sortingOrder = 90;
        }
    }

    public void SetRoomInfo(GameObject room)
    {
        roomObject = room;
        roomInfo = roomObject.GetComponent<Room>();
    }

    public void SetShopItem(int mode)
    {
        // 액티브/장신구/패시브 아이템 랜덤
        if (mode == 0)
        {
            cost = 15; // 비용 설정 ( 15원 )
            ItemCost(cost);
            CreateItem();
        }

        // 일회성 드랍템 
        else
        {
            cost = Random.Range(4, 11); // 비용 설정 ( 4 ~ 10 원 랜덤 )
            ItemCost(cost);
            CreateDropItem();
        }
    }

    void ItemCost(int cost)
    {
        string tmp = cost.ToString();
        if (tmp.Length == 1)
        {
            Cost_1.GetComponent<SpriteRenderer>().sprite = costImages[cost];
        }

        else
        {
            Cost_10.GetComponent<SpriteRenderer>().sprite = costImages[1];
            Cost_1.GetComponent<SpriteRenderer>().sprite = costImages[tmp[1] - '0'];
        }
    }

    public void CreateDropItem()
    {
        int rd = Random.Range(1, 4);
        item = ItemManager.instance.itemTable.GetDropItem(rd);
        item.GetComponent<Collider2D>().enabled = false; // 콜라이더 off
        item.transform.position = itemPos.position;
    }

    public void CreateItem()
    {
        // 오브젝트 생성
        item = Instantiate(ItemManager.instance.itemTable.OpenGoldChest()) as GameObject;

        item.GetComponent<Collider2D>().enabled = false; // 콜라이더 off
        item.GetComponent<Rigidbody2D>().velocity = Vector3.zero; // 초기화

        item.transform.SetParent(itemPos);
        item.transform.localPosition = Vector3.zero;

        Invoke("SetInfomation",0.2f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 구매 시
        if (collision.gameObject.CompareTag("Player") && ItemManager.instance.coinCount >= cost)
        {
            item.GetComponent<Collider2D>().enabled = true; // 아이템의 콜라이더를 다시 켜줌.
            item.transform.position = collision.transform.position; // 구매 직후 바로 플레이어가 획득할수있도록 플레이어 위치로 강제이동
            ItemManager.instance.coinCount -= cost; // 구매 비용 사용

            gameObject.layer = 31; // 상점 테이블과 충돌 못하도록 레이어 변경
        }
    }

    void SetInfomation()
    {
        if (item.GetComponent<ItemInfo>() != null)
        {
            itemInfomation[0] = item.GetComponent<ItemInfo>().itemTitle;
            itemInfomation[1] = item.GetComponent<ItemInfo>().itemInformation;
        }
        else if (item.GetComponent<ActiveInfo>() != null)
        {
            itemInfomation[0] = item.GetComponent<ActiveInfo>().itemTitle;
            itemInfomation[1] = item.GetComponent<ActiveInfo>().itemInformation;
        }
        else if (item.GetComponent<TrinketInfo>() != null)
        {
            itemInfomation[0] = item.GetComponent<TrinketInfo>().itemTitle;
            itemInfomation[1] = item.GetComponent<TrinketInfo>().itemInformation;
        }
    }
}
