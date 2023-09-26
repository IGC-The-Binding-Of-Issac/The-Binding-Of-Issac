using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaggysFaith : ItemInfo
{
    MaggysFaith()
    {
        itemCode = 20;
        itemName = "Maggy's Faith";
        item_Range = 0.3f;
        item_MaxHP = 2.0f;
    }
    //스테이지 클리어 시 최대 체력 + 1칸
}
