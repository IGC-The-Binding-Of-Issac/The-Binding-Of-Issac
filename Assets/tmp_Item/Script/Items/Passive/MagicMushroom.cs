using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicMushroom : ItemInfo
{
    MagicMushroom()
    {
        itemCode = 4;
        itemName = "Magic Mushroom";
        item_MoveSpeed = 0.3f;
        item_Damage = 0.3f;
        item_DamageMulti = 1.5f;
        item_Range = 2.5f;
        item_MaxHP = 2.0f;
    }
    //public float tearsSize = 1.1f;
    //public float playerSize = 1.1f;
    //현재 체력 전부 회복

}
