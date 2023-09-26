using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheBean : ItemInfo
{
    TheBean()
    {
        itemCode = 37;
        itemName = "The Bean";
    }

    //사용 시 캐릭터를 중심으로 2타일 내 범위에 독방귀를 낀다.
    //독 방귀는 초당 [playerDamage]만큼의 피해를 끼친다.
}
