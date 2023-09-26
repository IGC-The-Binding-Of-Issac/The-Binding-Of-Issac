using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlindRange : ItemInfo
{
    BlindRange()
    {
        itemCode = 23;
        itemName = "Blind Range";
        item_AttackSpeed = -0.3f;
        item_Range = -0.15f;
    }
    // 피격 후 무적시간이 2배가 된다.
}
