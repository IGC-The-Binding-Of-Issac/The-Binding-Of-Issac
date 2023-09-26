using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoyMilk : ItemInfo
{
  SoyMilk()
    {
        itemCode = 5;
        itemName = "Soy Milk";
        item_DamageMulti = 0.2f;
        item_AttackSpeed = 5.5f; //곱하기
    }
    //public float tearsSize = 0.3f;
    //충전이 필요한 공격 -> 충전 안해도 발동
}
