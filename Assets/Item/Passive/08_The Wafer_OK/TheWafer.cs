using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheWafer : ItemInfo
{
    public override void Start()
    {
        base.Start();
        SetItemCode(8);
        SetItemString("밀빵",
                      "탄수화물 덩어리",
                      "습득 시 이동속도 - 2"
                    + "\n공격력 + 2"
                    + "\n사거리 - 2");
    }
    public override void UseItem()
    {
        PlayerManager.instance.playerMoveSpeed -= 2f;
        PlayerManager.instance.playerDamage += 2f;
        PlayerManager.instance.CheckedDamage();
        PlayerManager.instance.playerRange -= 2f;
    }
}
