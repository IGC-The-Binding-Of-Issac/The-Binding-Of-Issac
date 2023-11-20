using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadCat : ItemInfo
{
    public override void Start()
    {
        base.Start();
        SetItemCode(14);
        SetItemString("죽은 고양이",
                      "구피가 죽었어...",
                      "습득 시 최대 체력이 1칸으로 고정"
                    + "\n사망 시 50% 확률로 최대체력 회복 / 사망");
    }

    public override void UseItem()
    {
        PlayerManager.instance.playerMaxHp = 2;
        PlayerManager.instance.playerHp = PlayerManager.instance.playerMaxHp;
        UIManager.instance.DelHeart();
    }
}
