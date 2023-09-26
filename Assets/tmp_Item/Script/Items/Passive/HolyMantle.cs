using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyMantle : ItemInfo
{
    HolyMantle()
    {
        itemCode = 7;
        itemName = "Holy Mantle";
    }
    //습득 시 오오라가 생김, 방을 돌 때마다 오오라가 생김
    //오오라가 있는 상태로 피격 시 1회 무시, 오오라가 사라지고 1초간 무적
}
