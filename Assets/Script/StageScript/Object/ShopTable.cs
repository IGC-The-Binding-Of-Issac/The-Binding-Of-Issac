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
                CreateItem(mode%3);

                roomInfo = null;
            }
        }
    }

    public void CreateItem(int mode)
    {
        // 액티브/장신구/패시브 아이템 랜덤
        if (mode == 0)
        {
            cost = 15; // 비용 설정 ( 15원 )
            string tmp = cost.ToString();
            if(tmp.Length == 1)
            {
                Cost_1.GetComponent<SpriteRenderer>().sprite = costImages[cost];
            }
            else
            {
                Cost_10.GetComponent<SpriteRenderer>().sprite = costImages[1];
                Cost_10.GetComponent<SpriteRenderer>().sprite = costImages[tmp[1] - '0'];
            }
        }

        // 일회성 드랍템 
        else
        {
            cost = Random.Range(4, 11); // 비용 설정 ( 4 ~ 10 원 랜덤 )
            string tmp = cost.ToString();
            if (tmp.Length == 1)
            {
                Cost_1.GetComponent<SpriteRenderer>().sprite = costImages[cost];
            }
            else
            {
                Cost_10.GetComponent<SpriteRenderer>().sprite = costImages[1];
                Cost_10.GetComponent<SpriteRenderer>().sprite = costImages[tmp[1] - '0'];
            }
        }
    }


    public void SetRoomInfo(GameObject room)
    {
        roomObject = room;
        roomInfo = roomObject.GetComponent<Room>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }
}
