using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadCat : ItemInfo
{
    DeadCat()
    {
        itemCode = 15;
        itemName = "Dead Cat";
    }
    //목숨 갯수 * 9개
    //최대 체력 1칸으로 변경
    //사망 후 부활 시 전 방으로 텔레포트된다.

}
