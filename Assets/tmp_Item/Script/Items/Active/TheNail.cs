using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheNail : ItemInfo
{
    TheNail()
    {
        itemCode = 26;
        itemName = "The Nail";
        item_Damage = 2.0f;
        item_MoveSpeed = -0.18f;
        item_MaxHP = 1.0f;
    }
    //사용 시
    //캐릭터가 악마로 변한다. + 날 수 있게 된다. (보류)
    //최대 체력 + 0.5칸
    //캐릭터의 눈물이 빨갛게 변한다.
    //돌을 부술 수 있게 된다. (보류)
}
