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

    [SerializeField]
    GameObject[] ActiveItems; // 액티브 아이템

    [SerializeField] private List<int> passive;
    [SerializeField] private List<int> trinket;
    [SerializeField] private List<int> active;
    private void Start()
    {
        // 중복 드랍 방지용  초기화
        passive = new List<int>();
        trinket = new List<int>();
        active = new List<int>();
        for(int i = 0; i < PassiveItems.Length; i++)
            passive.Add(i);

        for (int i = 0; i < TrinketItems.Length; i++)
            trinket.Add(i);

        for (int i = 0; i < ActiveItems.Length; i++)
            active.Add(i);
    }
    public GameObject ObjectBreak() // 오브젝트 부쉈을때
    {
        int rd = Random.Range(0, DropItems.Length-1);
        return DropItems[rd]; // 랜텀 아이템 반환 ( 열쇠 제외 )
    }

    public GameObject OpenNormalChest(int rd)
    {
        return DropItems[rd];
    }

    GameObject DropTrinket()
    {
        GameObject obj;
        if (trinket.Count == 0)
        {
            if(passive.Count == 0)
            {
                if(active.Count == 0)
                {
                    obj = DropItems[3];
                    return obj;
                }
                ShuffleList(ref active);
                obj = ActiveItems[active[0]];
                active.RemoveAt(0);
                return obj;
            }

            ShuffleList(ref passive);
            obj = PassiveItems[passive[0]];
            passive.RemoveAt(0);
            return obj;
        }

        ShuffleList(ref trinket);
        obj = TrinketItems[trinket[0]];
        trinket.RemoveAt(0);
        return obj;
    }

    GameObject DropPssive()
    {
        GameObject obj;
        if (passive.Count == 0)
        {
            if (active.Count == 0)
            {
                if (trinket.Count == 0)
                {
                    obj = DropItems[3];
                    return obj;
                }
                ShuffleList(ref trinket);
                obj = TrinketItems[trinket[0]];
                trinket.RemoveAt(0);
                return obj;
            }

            ShuffleList(ref active);
            obj = ActiveItems[active[0]];
            active.RemoveAt(0);
            return obj;
        }

        ShuffleList(ref passive);
        obj = PassiveItems[passive[0]];
        passive.RemoveAt(0);
        return obj;
    }

    GameObject DropActive()
    {
        GameObject obj;
        if (active.Count == 0)
        {
            if (passive.Count == 0)
            {
                if (trinket.Count == 0)
                {
                    obj = DropItems[3];
                    return obj;
                }
                ShuffleList(ref trinket);
                obj = TrinketItems[trinket[0]];
                trinket.RemoveAt(0);
                return obj;
            }
            ShuffleList(ref passive);
            obj = PassiveItems[passive[0]];
            passive.RemoveAt(0);
            return obj;
        }
        ShuffleList(ref active);
        obj = ActiveItems[active[0]];
        active.RemoveAt(0);
        return obj;
    }

    public GameObject OpenGoldChest()
    {
        // 드랍확률 : 장신구 50% / 패시브 25% / 액티브 25%
        // 패시브/액티브/장신구 : 한번이라도 드랍된아이템은 드랍X
        // 장신구 아이템이 전부 나왔으면, 패시브아이템 드랍
        // 패시브 아이템이 전부 나왔으면, 액티브아이템 드랍
        // 액티브 아이템이 전부 나왔으면, 열쇠드랍  
        // 장신구 -> 패시브 -> 액티브 -> 열쇠

        return DropTrinket();
        //int rd = Random.Range(0, 100000);
        //int num = rd % 100;

        //if (0 <= num && num <= 50) // 장신구 드랍
        //{
        //    return DropTrinket();
        //}

        //if (51 <= num && num <= 75) // 패시브 드랍
        //{
        //    return DropPssive();
        //}

        //if (75 <= num && num <= 100) // 액티브 드랍 
        //{
        //    return DropActive();
        //}
        //return DropItems[3];
    }

    public GameObject TrinketChange(int itemCode)
    {
        return TrinketItems[itemCode];
    }

    private void ShuffleList(ref List<int> list)
    {
        int rd1, rd2;
        int tmp;

        for(int i = 0; i < list.Count; i++)
        {
            rd1 = Random.Range(0, list.Count);
            rd2 = Random.Range(0, list.Count);

            tmp = list[rd1];
            list[rd1] = list[rd2];
            list[rd2] = tmp;
        }
    }
   
}