using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheD6 : ItemInfo
{
    TheD6()
    {
        itemCode = 25;
        itemName = "The Dice";
    }

    //사용 시 방 안에 있는 모든 액티브/패시브 아이템들을 같은 방 배열의 랜덤한 아이템으로 바꾼다.
}
