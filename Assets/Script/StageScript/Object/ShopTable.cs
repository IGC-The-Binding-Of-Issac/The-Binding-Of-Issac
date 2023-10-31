using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTable : MonoBehaviour
{
    GameObject roomObject;
    Room roomInfo;
    [Header("State")]
    [SerializeField] int cost;
    [SerializeField] GameObject item;

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
        item = Instantiate(ItemManager.instance.itemTable.OpenNormalChest(rd)) as GameObject;
        item.GetComponent<Collider2D>().enabled = false; // 콜라이더 off
        item.transform.SetParent(itemPos);

        StartCoroutine(VeryFuckingDropItem());
    }

    IEnumerator VeryFuckingDropItem()
    { 
        for(int i = 0; i < 15; i++)
        {
            item.GetComponent<Rigidbody2D>().velocity = Vector3.zero; // 초기화
            item.transform.localPosition = Vector3.zero;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void CreateItem()
    {
        // 오브젝트 생성
        item = Instantiate(ItemManager.instance.itemTable.OpenGoldChest()) as GameObject;

        item.GetComponent<Collider2D>().enabled = false; // 콜라이더 off
        item.GetComponent<Rigidbody2D>().velocity = Vector3.zero; // 초기화

        item.transform.SetParent(itemPos);
        item.transform.localPosition = Vector3.zero;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && ItemManager.instance.coinCount >= cost)
        {
            item.GetComponent<Collider2D>().enabled = true;
            itemPos.localPosition = Vector3.zero;
            ItemManager.instance.coinCount -= cost;
            Destroy(this);
        }
    }
}
