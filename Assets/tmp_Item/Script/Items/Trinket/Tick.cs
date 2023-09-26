using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tick : ItemInfo
{
   Tick()
    {
        itemCode = 17;
        itemName = "Tick";
    }
    
    //한 번 드랍 시 교체 / 해제가 불가능하다.
    //보스의 체력이 60 이상인 경우 10% 깎여서 등장한다.
}
