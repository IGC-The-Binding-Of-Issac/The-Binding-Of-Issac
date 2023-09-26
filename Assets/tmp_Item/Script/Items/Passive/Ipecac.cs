using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ipecac : ItemInfo
{
    Ipecac()
    {
        itemCode = 10;
        itemName = "Ipecac";
        item_Hp = -0.5f; //30초마다
        item_AttackSpeed = -0.1f;
    }
    //공격이 독 속성을 지닌다
    //독 공격은 초당 [PlayerDamage]만큼의 피해를 입힌다.
}
