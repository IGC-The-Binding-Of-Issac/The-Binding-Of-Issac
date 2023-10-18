using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTable : MonoBehaviour
{
    [SerializeField]
    GameObject[] DropItems; // 드랍아이템
    // 0 : 코인 , 1 : 하트 , 2 : 폭탄 , 3 : 열쇠

    [SerializeField]
    GameObject[] PassiveItems; // 패시브 아이템

    [SerializeField]
    GameObject[] TrinketItems; // 장신구 아이템

    public GameObject ObjectBreak() // 오브젝트 부쉈을때
    {
        int rd = Random.Range(0, DropItems.Length-1);
        return DropItems[rd]; // 랜텀 아이템 반환 ( 열쇠 제외 )
    }

    public GameObject OpenNormalChest(int rd)
    {
        return DropItems[rd];
    }

    public GameObject TrinketChange(int itemCode)
    {
        return TrinketItems[itemCode];
    }
    //* 보유 현황에 따른 아이템 중복 드랍 방지
}