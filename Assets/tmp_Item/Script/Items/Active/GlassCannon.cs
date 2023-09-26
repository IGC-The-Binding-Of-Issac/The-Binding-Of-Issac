using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassCannon : ItemInfo
{
    GlassCannon()
    {
        itemCode = 27;
        itemName = "Glass Cannon";
    }

    //사용 시 커다란 눈물을 발사, 발사된 눈물의 공격력은 [PlayerDamage * 10 + 10]의 데미지를 준다.
}
