using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuppyTail : ItemInfo
{
    GuppyTail()
    {
        itemCode = 16;
        itemName = "Guppy's Tail";
        item_MoveSpeed = 1.0f;
    }
    //습득 후, 캐릭터가 사망할 때마다 50% 확률로 부활한다.
}
