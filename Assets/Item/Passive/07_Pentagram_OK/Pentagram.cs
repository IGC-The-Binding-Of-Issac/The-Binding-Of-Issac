using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pentagram : ItemInfo
{
    public override void Start()
    {
        base.Start();
        SetItemCode(7);
        SetItemString("오망성",
                      "공격력 증가",
                      "습득 시 공격력 + 1" 
                    + "\n최대 체력 + 1칸");
    }
    public override void UseItem()
    {
        PlayerManager.instance.playerDamage += 1.0f;
        PlayerManager.instance.playerMaxHp += 2;
        UIManager.instance.AddHeart();
    }
}
