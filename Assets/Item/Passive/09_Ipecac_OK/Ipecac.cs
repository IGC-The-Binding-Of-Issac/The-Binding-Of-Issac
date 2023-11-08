using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ipecac : ItemInfo
{
    private void Start()
    {
       
        SetItemCode(9);
        SetItemString("구토제",
            "토할 것 같아..",
            "습득 시 체력 반 칸 감소" +
            "\n외형이 누렇게 변한다.");
    }

    public override void UseItem()
    {
        PlayerManager.instance.playerHp--;
        PlayerManager.instance.SetHeadSkin(1);
        PlayerManager.instance.SetBodySkin(1);
        //공격이 독 속성을 지닌다. 
        //독 공격은 초당 [playerDamage] 만큼의 피해를 입힌다.
    }
}