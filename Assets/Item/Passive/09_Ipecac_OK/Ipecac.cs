using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ipecac : ItemInfo
{
    public Sprite ipecacSpr;
    private void Start()
    {
       
        SetItemCode(9);
        SetItemString("구토제",
                      "토할 것 같아..",
                      "습득 시 체력 반 칸 감소" 
                    + "\n외형이 누렇게 변한다.");
    }

    public override void UseItem()
    {
        PlayerManager.instance.playerHp--;
        PlayerManager.instance.playerTearSpeed *= 2f;
        PlayerManager.instance.CheckedPlayerHP();
        PlayerManager.instance.SetHeadSkin(1);
        PlayerManager.instance.SetBodySkin(1);
        PlayerManager.instance.SetTearSkin(2);
        //눈물이 포물선을 그리며 날아감
        //포물선 마지막 지점에서 터지며 데미지를 줌
    }
}