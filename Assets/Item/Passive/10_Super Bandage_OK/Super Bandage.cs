using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperBandage : ItemInfo
{

    public override void Start()
    {
        base.Start();
        SetItemCode(10);
        SetItemString("슈퍼 반창고",
                      "그만 때려! 너무 아파!",
                      "습득 시 최대 체력 - 1칸"
                    + "\n체력 - 1칸"
                    + "\n공격속도 + 0.1");
    }
    public override void UseItem()
    {
        PlayerManager.instance.playerMaxHp-=2;
        PlayerManager.instance.playerHp-=2;
        UIManager.instance.DelHeart();
        UIManager.instance.SetPlayerCurrentHP();
        PlayerManager.instance.playerShotDelay-=0.1f;
        base.UseItem();

        if (!ItemManager.instance.PassiveItems[16])
        {
            PlayerManager.instance.SetTearSkin(1);
        }
    }
}
