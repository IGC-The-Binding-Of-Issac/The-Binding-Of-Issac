using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AAABattery : ItemInfo
{
    AAABattery()
    {
        itemCode = 21;
        itemName = "AAA Battery";
        item_AttackSpeed = 0.15f;
    }
    //배터리가 충전까지 1칸 부족하다면 마저 채워준다.
}
