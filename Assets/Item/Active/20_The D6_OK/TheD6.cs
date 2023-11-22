using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheD6 : ActiveInfo
{

    public LayerMask itemMask;
    public void Awake()
    {
        base.Start();
        SetActiveItem(20, 6);
        SetActiveString("The D6",
                        "운명을 바꾸는 능력",
                        "방 안의 모든 장착 아이템을" +
                        "\n다른 아이템으로 변경한다.");
    }

    public override void UseActive()
    {
        if(canUse)
        {
            //모든 방 중에서 현재 방 정보를 불러온 후 사이즈를 측정하고
            GameObject d6Room = GetRoom();
            Vector2 size = GetSize(d6Room);

            //그 Size와 충돌하는 모든 아이템들 찾아낸 후 아이템 변경
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
            //변경된 아이템은 itemList에 추가해서 생성된 아이템으로 관리.
            if(newItems != null)
            {
                GameManager.instance.roomGenerate.itemList.Add(newItems);
                Destroy(col.gameObject);
            }
            
        }
    }
}
