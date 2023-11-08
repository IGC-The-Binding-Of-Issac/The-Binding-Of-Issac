using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheD6 : ActiveInfo
{

    public LayerMask itemMask;
    private void Awake()
    {
        SetActiveItem(20, 6);
        SetActiveString("The D6",
                        "운명을 바꾸는 능력",
                        "사용 시 방 안의 모든 액티브/패시브/장신구 아이템을" +
                        "\n다른 아이템으로 변경한다.");
    }

    public override void UseActive()
    {
        if(canUse)
        {
            GameObject d6Room = GetRoom();
            Vector2 size = GetSize(d6Room);

            Collider2D[] itemColliders = Physics2D.OverlapBoxAll(d6Room.transform.position, size, 0, itemMask);
            ChangeItems(itemColliders);
        }
    }

    GameObject GetRoom()
    {
        Transform rooms = GameManager.instance.roomGenerate.roomPool;
        for (int i = 0; i < rooms.childCount; i++)
        {
            if (rooms.GetChild(i).GetComponent<Room>().playerInRoom)
            {
                return rooms.GetChild(i).gameObject;
            }
        }
        return null;
    }

    Vector2 GetSize(GameObject obj)
    {
        return obj.GetComponent<BoxCollider2D>().size;
    }

    void ChangeItems(Collider2D[] collider)
    {
        foreach (Collider2D col in collider)
        {
            GameObject newItems = null;
            Vector3 colPos = col.transform.position;
            if (col.GetComponent<ItemInfo>() != null)
            {
                newItems = Instantiate(ItemManager.instance.itemTable.DropPassive(),colPos, Quaternion.identity) as GameObject;
                
            }
            else if (col.GetComponent<ActiveInfo>() != null)
            {
                newItems = Instantiate(ItemManager.instance.itemTable.DropActive(), colPos, Quaternion.identity) as GameObject;
            }
            else if (col.GetComponent<TrinketInfo>() != null)
            {
                newItems = Instantiate(ItemManager.instance.itemTable.DropTrinket(), colPos, Quaternion.identity) as GameObject;
            }

            if(newItems != null)
            {
                GameManager.instance.roomGenerate.itemList.Add(newItems);
                Destroy(col.gameObject);
            }
            
        }
    }
}
