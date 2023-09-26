using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrimStone : ItemInfo
{
    BrimStone()
    {
        itemCode = 12;
        itemName = "BrimStone";
        item_DamageMulti = 0.33f;
    }
    // 혈사포 : 틱당 [playerDamage]의 피해를 입히고
    // 혈사포 1발 당 9틱의 지속시간이 존재.
    // 충전(2초)이 완료되어야 발사, 
    // 그 전에 발사할 경우 충전이 취소된다.
    // 충전 시간은 공격 속도에 반비례한다.
}
