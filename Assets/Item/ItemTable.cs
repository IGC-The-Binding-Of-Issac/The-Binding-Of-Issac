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


    public GameObject OpenGoldChest()
    {
        // 드랍확률 : 장신구 60% / 패시브 20% / 액티브 20%
        // 패시브 : 이미 보유중인 아이템 중복드랍 X
        // 액티브/장신구 : 한번이라도 먹은아이템은 드랍X
        // 장신구 아이템이 전부 나왔으면, 패시브아이템 드랍
        // 패시브 아이템이 전부 나왔으면, 액티브아이템 드랍
        // 액티브 아이템이 전부 나왔으면, 열쇠드랍  

        // 장신구 -> 패시브 -> 액티브 -> 열쇠
        return PassiveItems[1];
    }



    public GameObject TrinketChange(int itemCode)
    {
        return TrinketItems[itemCode];
    }
}