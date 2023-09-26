using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoopDaWhoop : ItemInfo
{
   ShoopDaWhoop()
    {
        itemCode = 31;
        itemName = "Shoop Da Whoop";
    }
    //사용 시 캐릭터의 얼굴이 변화
    //해당 방향으로 레이저를 발사
    //레이저는 초당 [playerDamage * 5]의 피해를 입힌다.
}
