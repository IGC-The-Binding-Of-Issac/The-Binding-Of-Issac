using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeremonialRobes : ItemInfo
{

    public override void Start()
    {
        base.Start();
        SetItemCode(5);
        SetItemString("의식용 예복",
                      "어두운 두건",
                      "습득 시 최대 체력 + 2칸"
                    + "\n공격력 + 1");
    }

    public override void UseItem()
    {
        PlayerManager.instance.playerMaxHp += 4;
        UIManager.instance.AddHeart();
        PlayerManager.instance.playerDamage += 1;
        PlayerManager.instance.CheckedDamage();
        //캐릭터 외형 변경
        PlayerManager.instance.SetBodySkin(3);
        PlayerManager.instance.SetHeadSkin(5);
    }
}
