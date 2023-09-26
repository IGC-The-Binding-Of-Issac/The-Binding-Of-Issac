using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicFingers : ItemInfo
{
    MagicFingers()
    {
        itemCode = 34;
        itemName = "Magic Fingers";
        item_Damage = 0.03f;
    }
    //사용 시 1원을 소모하여 playerDamage + 0.03f;
}
