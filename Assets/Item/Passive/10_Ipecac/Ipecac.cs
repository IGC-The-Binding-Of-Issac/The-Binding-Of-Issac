using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ipecac : ItemInfo
{
    private float time = 0f;
    private void Start()
    {
        itemCode = 10;
        // SetItemCode(10);
    }

    public override void UseItem()
    {
            PlayerManager.instance.playerHp--;
        //공격이 독 속성을 지닌다. 
        //독 공격은 초당 [playerDamage] 만큼의 피해를 입힌다.
    }
}