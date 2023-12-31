using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CricketsHead : ItemInfo
{
    public override void Start()
    {
        base.Start();
        SetItemCode(1);
        SetItemString("크리켓의 머리",
                      "공격력 증가",
                      "습득 시 눈이 더 커진다." 
                    + "\n공격력 + 0.5" 
                    + "\n공격력 * 1.3" 
                    + "\n눈물 크기 * 1.1배");
    }
    public override void UseItem()
    {
        PlayerManager.instance.playerDamage += 0.5f;
        PlayerManager.instance.playerDamage *= 1.3f;
        PlayerManager.instance.playerTearSize *= 1.1f;
        base.UseItem();
        PlayerManager.instance.ChgTearSize();
        PlayerManager.instance.SetHeadSkin(3);
    }
}
